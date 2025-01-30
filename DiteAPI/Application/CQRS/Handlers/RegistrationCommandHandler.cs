using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private UserManager<GenericUser> _userManager;
        private readonly ILogger<RegistrationCommand> _logger;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ISessionService _sessionService;
        private readonly AppSettings _appSettings;

        public RegistrationCommandHandler(
            DataDBContext dbContext,
            UserManager<GenericUser> userManager,
            ILogger<RegistrationCommand> logger,
            IAccountService accountService,
            IEmailService emailService,
            IConfiguration configuration,
            ISessionService sessionService,
            IOptions<AppSettings> options
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
            _accountService = accountService;
            _emailService = emailService;
            _configuration = configuration;
            _sessionService = sessionService;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    // Check if Email Address is a disposable Email
                    /*if (!(await _emailService.CheckDisposableEmailAsync(request.Email!)).Status)
                        return new BaseResponse<string>(false, "The provided email address is not currently supported.");
*/
                    var user = new GenericUser()
                    {
                        //Username = request.Username,
                        //LastName = request.LastName,
                        //MiddleName = request.MiddleName,
                        Email = request.Email,
                        NormalizedEmail = request.Email!.ToUpperInvariant(),
                        //DateOfBirth = request.DateOfBirth,
                        //UserGender = request.UserGender,
                        UserName = request.UserName,
                        NormalizedUserName = request.UserName!.ToUpperInvariant(),
                        //PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = false
                    };
                    await _dbContext.AddAsync(user, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    // Persist password
                    var addPassword = await _userManager.AddPasswordAsync(user, request.Password!);
                    if (!addPassword.Succeeded)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new BaseResponse(false, _appSettings.ProcessingError);
                    }

                    // Store user details in session
                    _sessionService.SetStringInSession("UserId", user.Id.ToString());
                    _sessionService.SetStringInSession("IsOtpVerified", "false");

                    // Send Verification Email to the New User email
                    var sendEmail = await _accountService.SendOTPAsync(new SendOTPToUser
                    {
                        UserId = user.Id,
                        FirstName = request.UserName,
                        Recipient = request.Email,
                        Purpose = VerificationPurposeEnum.EmailConfirmation,
                        OtpCodeLength = OtpCodeLengthEnum.Six,
                        RecipientType = OtpRecipientTypeEnum.Email

                    }, cancellationToken);
                    if (!sendEmail.Status)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new BaseResponse(false, sendEmail.Message!);

                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse(true, _appSettings.RegistrationSuccessfully);
                }
                catch(Exception ex)
                {
                    _logger.LogError($"REGISTRATION_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"REGISTRATION_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
