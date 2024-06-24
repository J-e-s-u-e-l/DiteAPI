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
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly IAccountService _accountService;
        private readonly ISessionService _sessionService;
        private readonly AppSettings _appSettings;

        public VerifyOtpCommandHandler(DataDBContext dbContext, UserManager<GenericUser> userManager, ILogger<RegistrationCommand> logger, IAccountService accountService, IEmailService emailService, ISessionService sessionService, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _accountService = accountService;
            _sessionService = sessionService;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var userIdFromSession = _sessionService.GetStringFromSession("UserId");
            if (userIdFromSession == null)
                return new BaseResponse(false, $"{_appSettings.NoActiveSession}");

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == Guid.Parse(userIdFromSession));
            if (user is null)
                return new BaseResponse(false, _appSettings.NoActiveSession);

            var validateOtp = new BaseResponse(false, "");

            switch (request.Purpose)
            {
                case VerificationPurposeEnum.EmailConfirmation:
                    validateOtp = await _accountService.ValidateCodeAsync(new ValidateCodeRequest
                    {
                        Code = request.Code,
                        Purpose = VerificationPurposeEnum.EmailConfirmation,
                        UserId = user.Id
                    }, cancellationToken);

                    if (!validateOtp.Status)
                        return new BaseResponse(false, validateOtp.Message ?? $"{_appSettings.InvalidOtp}");

                    user.EmailConfirmed = true;
                    user.TimeUpdated = DateTimeOffset.UtcNow;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                break;

                case VerificationPurposeEnum.PasswordReset:
                    validateOtp = await _accountService.ValidateCodeAsync(new ValidateCodeRequest
                    {
                        Code = request.Code,
                        Purpose = VerificationPurposeEnum.PasswordReset,
                        UserId = user.Id
                    }, cancellationToken);

                    if (!validateOtp.Status)
                        return new BaseResponse(false, validateOtp.Message ?? $"{_appSettings.InvalidOtp}");

                    _sessionService.SetStringInSession("IsOtpVerified", "true");
                break;
            }

            if (request.Purpose == VerificationPurposeEnum.EmailConfirmation)
                return new BaseResponse(true, $"{_appSettings.EmailVerified}");

            else
                return new BaseResponse(true, $"{_appSettings.OtpVerified}");
        }
    }
}