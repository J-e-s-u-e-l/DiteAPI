using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly DataDBContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IHelperMethods _helperMethods;
        private readonly IConfiguration _configuration;
        private readonly ContactInformation _contactInformation;

        public AccountService(
            ILogger<AccountService> logger,
            DataDBContext dbContext,
            IEmailService emailService,
            IHelperMethods helperMethods,
            IOptions<ContactInformation> contactInformation,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailService = emailService;
            _helperMethods = helperMethods;
            _contactInformation = contactInformation.Value;
            _configuration = configuration;
        }

        public async Task<BaseResponse<OtpRequestResult>> SendOTPAsync(SendOTPToUser request, CancellationToken cancellationToken)
        {
            var code = request.OtpCodeLength == OtpCodeLengthEnum.Four ? _helperMethods.GenerateRandomNumber(4) : _helperMethods.GenerateRandomNumber(6);
            _logger?.LogInformation($"EmailService > Sending OTP to {request.Recipient}");


            //Invalidate existing sent OTPs
            await _dbContext.OtpVerifications.Where(x => x.UserId.Equals(request.UserId) && x.Purpose == request.Purpose && x.Status == OtpTokenStatusEnum.Sent)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(column => column.Status, OtpTokenStatusEnum.Invalidated)
                .SetProperty(column => column.TimeUpdated, DateTimeOffset.UtcNow), cancellationToken);

            // Persist new OTP
            var otpVerification = new OtpVerification
            {
                UserId = request.UserId,
                Code = code,
                Recipient = request.Recipient,
                RecipientType = request.RecipientType,
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Status = OtpTokenStatusEnum.Sent,
                Purpose = request.Purpose
            };
                await _dbContext.AddAsync(otpVerification, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.RecipientType == OtpRecipientTypeEnum.Email)
            {
                // Generate email body and send email to user
                await SendEmailOtpAsync(request, code, "Dite" ?? "");
            }

            _logger?.LogInformation($"AccountService > OTP sent to {request.Recipient}");
            return new BaseResponse<OtpRequestResult>(true, "OTP sent successfully", new OtpRequestResult
            {
                Recipent = request.Recipient
            });
        }
        private async Task SendEmailOtpAsync(SendOTPToUser request, string code, string applicationName)
        {
            var emailBodyRequest = new EmailBodyRequest { Email = request.Recipient, FirstName = request.FirstName };

            var emailBodyResponse = new BaseResponse<EmailBodyResponse>();

            switch (request.Purpose)
            {
                case VerificationPurposeEnum.EmailConfirmation:
                    emailBodyRequest.EmailTitle = EmailTitleEnum.EMAILVERIFICATION;
                    emailBodyResponse = await _emailService.GetEmailBody(emailBodyRequest);
                    break;

                case VerificationPurposeEnum.PasswordReset:
                    emailBodyRequest.EmailTitle = EmailTitleEnum.PASSWORDRESET;
                    emailBodyResponse = await _emailService.GetEmailBody(emailBodyRequest);
                    break;

                default: break;
            }

            if (!emailBodyResponse.Status)
            {
                _logger.LogInformation($"AccountService => Sending OTP to {request.Recipient} failed");
            }
            else
            {
                var emailBody = emailBodyResponse.Data;

                string? htmlEmailBody = emailBody!.HtmlBody;
                string? plainEmailBody = emailBody.PlainBody;

                // Replace placeholders with values
                htmlEmailBody = htmlEmailBody?.Replace("[[CODE]]", code.ToString());
                Console.WriteLine(htmlEmailBody);
                plainEmailBody = plainEmailBody?.Replace("[[CODE]]", code);
                Console.WriteLine(plainEmailBody);

                htmlEmailBody = htmlEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);
                Console.WriteLine(htmlEmailBody);
                plainEmailBody = plainEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);
                Console.WriteLine(plainEmailBody);

                htmlEmailBody = htmlEmailBody?.Replace("[[CONTACT INFORMATION]]", _contactInformation.EmailAddress);
                Console.WriteLine(htmlEmailBody);
                plainEmailBody = plainEmailBody?.Replace("[[CONTACT INFORMATION]]", _contactInformation.EmailAddress);
                Console.WriteLine(plainEmailBody);

                // Send email to user
                await _emailService.SendEmailAsync(new SingleEmailRequest
                {
                    RecipientEmailAddress = request.Recipient,
                    RecipientName = request.FirstName,
                    EmailSubject = $"[{applicationName}] {request.Purpose.GetDescription()}",
                    HtmlEmailBody = htmlEmailBody,
                    PlainEmailBody = plainEmailBody
                });
            }
        }

        public async Task<BaseResponse<VerificationLinkRequestResult>> SendVerificationLinkAsync(SendVerificationLinkToUser request, CancellationToken cancellationToken)
        {
            var token = _helperMethods.GenerateVerificationToken(32);
            _logger?.LogInformation($"ACCOUNT_SERVICE > Sending token to {request.Recipient}");

            /* Invalidate existing sent tokens */
            await _dbContext.VerificationTokens.Where(x => x.UserId.Equals(request.UserId) && x.Purpose == request.VerificationPurpose && x.Status == OtpTokenStatusEnum.Sent)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(column => column.Status, OtpTokenStatusEnum.Invalidated)
                .SetProperty(column => column.TimeUpdated, DateTimeOffset.UtcNow), cancellationToken);

            // Persist new Verification link
            var verificationLink = new VerificationTokens
            {
                UserId = request.UserId,
                Token = token,
                Recipient = request.Recipient,
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Status = OtpTokenStatusEnum.Sent,
                Purpose = request.VerificationPurpose
            };
            await _dbContext.AddAsync(verificationLink, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Generate email body
            await SendEmailVerificationTokenAsync(request, token, "Dite" ?? "");

            _logger?.LogInformation($"ACCOUNT_SERVICE > OTP sent to {request.Recipient}");
            return new BaseResponse<VerificationLinkRequestResult>(true, "Email verification token sent successfully", new VerificationLinkRequestResult
            {
                Recipent = request.Recipient
            });

        }

        private async Task SendEmailVerificationTokenAsync(SendVerificationLinkToUser request, string token, string applicationName)
        {
            var emailBodyRequest = new EmailBodyRequest { Email = request.Recipient, FirstName = request.FirstName };

            var emailBodyResponse = new BaseResponse<EmailBodyResponse>();

            switch (request.VerificationPurpose)
            {
                case VerificationPurposeEnum.EmailConfirmation:
                    emailBodyRequest.EmailTitle = EmailTitleEnum.EMAILVERIFICATION;
                    emailBodyResponse = await _emailService.GetEmailBody(emailBodyRequest);
                    break;

                case VerificationPurposeEnum.PasswordReset:
                    emailBodyRequest.EmailTitle = EmailTitleEnum.PASSWORDRESET;
                    emailBodyResponse = await _emailService.GetEmailBody(emailBodyRequest);
                    break;

                default: break;
            }

            if (!emailBodyResponse.Status)
            {
                _logger.LogInformation($"ACCOUNT_SERVICE => Sending token to {request.Recipient} failed");
            }
            else
            {
                var emailBody = emailBodyResponse.Data;

                string? htmlEmailBody = emailBody!.HtmlBody;
                string? plainEmailBody = emailBody.PlainBody;

                // Get link to be sent to the user
                var getVerificationUrlTemplate = _configuration["Verification:VerificationLinkTemplate"];
                var verificationUrl = getVerificationUrlTemplate!.Replace("{token}", token);

                // Replace placeholders with values
                htmlEmailBody = htmlEmailBody?.Replace("[[LINK]]", verificationUrl);
                plainEmailBody = plainEmailBody?.Replace("[[LINK]]", verificationUrl);

                htmlEmailBody = htmlEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);
                plainEmailBody = plainEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);

                htmlEmailBody = htmlEmailBody?.Replace("[[CONTACT INFORMATION]]", _configuration["ContactInformation:EmailAddress"]);
                Console.WriteLine(htmlEmailBody);
                plainEmailBody = plainEmailBody?.Replace("[[CONTACT INFORMATION]]", _configuration["ContactInformation:EmailAddress"]);
                Console.WriteLine(plainEmailBody);

                await _emailService.SendEmailAsync(new SingleEmailRequest
                {
                    RecipientEmailAddress = request.Recipient,
                    RecipientName = request.FirstName,
                    EmailSubject = $"[{applicationName}] {request.VerificationPurpose.GetDescription()}",
                    HtmlEmailBody = htmlEmailBody,
                    PlainEmailBody = plainEmailBody
                });
            }
        }

        public async Task<BaseResponse> SendWelcomeEmailAsync(SendWelcomeRequest request, string applicationName, CancellationToken cancellationToken)
        {
            var emailBodyRequest = new EmailBodyRequest { Email = request.Email, FirstName = request.FirstName, EmailTitle = EmailTitleEnum.WELCOME };
            var emailBodyResponse = await _emailService.GetEmailBody(emailBodyRequest);
            var emailBody = emailBodyResponse.Data;

            if (!emailBodyResponse.Status)
                return new BaseResponse(false, emailBodyResponse.Message!);

            string? htmlEmailBody = emailBody!.HtmlBody;
            string? plainEmailBody = emailBody!.PlainBody;

            htmlEmailBody = htmlEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);
            plainEmailBody = plainEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);

            htmlEmailBody = htmlEmailBody?.Replace("[[CONTACT INFORMATION]]", _configuration["ContactInformation:EmailAddress"]);
            Console.WriteLine(htmlEmailBody);
            plainEmailBody = plainEmailBody?.Replace("[[CONTACT INFORMATION]]", _configuration["ContactInformation:EmailAddress"]);
            Console.WriteLine(plainEmailBody);

            await _emailService.SendEmailAsync(new SingleEmailRequest
            {
                RecipientEmailAddress = request.Email,
                RecipientName = request.FirstName,
                EmailSubject = $"Welcome to {applicationName}!"
            });

            return new BaseResponse(true, "Sent");
        }

        public async Task<BaseResponse> ValidateCodeAsync(ValidateCodeRequest tokenRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ACCOUNT_SERVICE Validating code => Process started");

            var codeVerification = await _dbContext.OtpVerifications.FirstOrDefaultAsync(v => v.UserId == tokenRequest.UserId && v.Status == OtpTokenStatusEnum.Sent && v.Purpose == tokenRequest.Purpose && v.Code.Equals(tokenRequest.Code), cancellationToken);
            if (codeVerification == null)
            {
                _logger.LogInformation($"ACCOUNT_SERVICE Validating code => No pending code found for this user");
                return new BaseResponse(false, "The verification code entered is invalid.");
            }

            // Check expiry
            if (DateTime.UtcNow > codeVerification.TimeCreated.AddMinutes(15))
            {
                _logger.LogInformation($"ACCOUNT_SERVICE => Validating code failed. Validation code has expired");

                codeVerification.Status = OtpTokenStatusEnum.Expired;
                codeVerification.TimeUpdated = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new BaseResponse(false, "The provided code has expired.");
            }

            codeVerification.ConfirmedOn = DateTime.UtcNow;
            codeVerification.Status = OtpTokenStatusEnum.Verified;
            codeVerification.TimeUpdated = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"ACCOUNT_SERVICE Validating  code => code authenticated successfully");
            return new BaseResponse(true, "Token verification was successful.");
        }
    }
}
