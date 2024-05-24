using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
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
        //private readonly IOptions<ContactInformation> _contactInformation;
        private readonly IConfiguration _configuration;


        public AccountService(
            ILogger<AccountService> logger,
            DataDBContext dbContext,
            IEmailService emailService,
            IHelperMethods helperMethods,
            //IOptions<ContactInformation> contactInformation
            IConfiguration configuration
            )
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailService = emailService;
            _helperMethods = helperMethods;
            //_contactInformation = contactInformation;
            _configuration = configuration;
        }

        public async Task<BaseResponse<OtpRequestResult>> SendOTPAsync(SendOTPRequest request, CancellationToken cancellationToken)
        {
            var code = request.OtpCodeLength == OtpCodeLengthEnum.Four ? _helperMethods.GenerateRandomNumber(4) : _helperMethods.GenerateRandomNumber(6);
            _logger?.LogInformation($"EmailService > Sending OTP to {request.Recipient}");

            /* Invalidate existing sent OTPs */
            await _dbContext.OtpVerifications.Where(x => x.UserId.Equals(request.UserId) && x.Purpose == request.Purpose && x.Status == OtpCodeStatusEnum.Sent)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(column => column.Status, OtpCodeStatusEnum.Invalidated)
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
                Status = OtpCodeStatusEnum.Sent,
                Purpose = request.Purpose
            };
            await _dbContext.AddAsync(otpVerification, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.RecipientType == OtpRecipientTypeEnum.Email)
            {
                // Generate email body
                await SendEmailOtpAsync(request, code, "Dite" ?? "");
            }

            _logger?.LogInformation($"AccountService > OTP sent to {request.Recipient}");
            return new BaseResponse<OtpRequestResult>(true, "OTP sent successfully", new OtpRequestResult
            {
                Recipent = request.Recipient
            }); 
        }

        private async Task SendEmailOtpAsync(SendOTPRequest request, string code, string applicationName)
        {
            var emailBodyRequest = new EmailBodyRequest { Email = request.Recipient, FirstName = request.FirstName };

            var emailBodyResponse = new BaseResponse<EmailBodyResponse>();

            switch (request.Purpose)
            {
                case OtpVerificationPurposeEnum.EmailConfirmation:
                    emailBodyRequest.EmailTitle = EmailTitleEnum.EMAILVERIFICATION;
                    emailBodyResponse = await _emailService.GetEmailBody(emailBodyRequest);
                break;

                case OtpVerificationPurposeEnum.PasswordResetRequest:
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

                htmlEmailBody = htmlEmailBody?.Replace("[[CONTACT INFORMATION]]", _configuration["ContactInformation:EmailAddress"]);
                Console.WriteLine(htmlEmailBody);
                plainEmailBody = plainEmailBody?.Replace("[[CONTACT INFORMATION]]", _configuration["ContactInformation:EmailAddress"]);
                Console.WriteLine(plainEmailBody);

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
            Console.WriteLine(htmlEmailBody);
            plainEmailBody = plainEmailBody?.Replace("[[RECIPIENT NAME]]", request.FirstName);
            Console.WriteLine(plainEmailBody);

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

        public async Task<BaseResponse> ValidateOTPCodeAsync(ValidateOtpRequest otpRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ACCOUNT_SERVICE Validating OTP Code => Process started");

            var otpVerification = await _dbContext.OtpVerifications.FirstOrDefaultAsync(v => v.UserId == otpRequest.UserId && v.Status == OtpCodeStatusEnum.Sent && v.Purpose == otpRequest.Purpose && v.Code.Equals(otpRequest.Code), cancellationToken);
            if (otpVerification == null)
            {
                _logger.LogInformation($"ACCOUNT_SERVICE Validating OTP Code => No pending OTP found for this user");
                return new BaseResponse(false, "The verification code entered is invalid.");
            }

            // Check expiry
            if (DateTime.UtcNow > otpVerification.TimeCreated.AddMinutes(15))
            {
                _logger.LogInformation($"ACCOUNT_SERVICE => Validating OTP failed. OTP Validation code has expired");

                otpVerification.Status = OtpCodeStatusEnum.Expired;
                otpVerification.TimeUpdated = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new BaseResponse(false, "The provided code has expired.");
            }

            otpVerification.ConfirmedOn = DateTime.UtcNow;
            otpVerification.Status = OtpCodeStatusEnum.Verified;
            otpVerification.TimeUpdated = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"ACCOUNT_SERVICE Validating OTP Code => Code authenticated successfully");
            return new BaseResponse(true, "Code verification was successful.");
        }

    }
}
