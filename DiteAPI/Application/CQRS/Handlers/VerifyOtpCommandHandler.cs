using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly IAccountService _accountService;

        public VerifyOtpCommandHandler(DataDBContext dbContext, UserManager<GenericUser> userManager, ILogger<RegistrationCommand> logger, IAccountService accountService, IEmailService emailService)
        {
            _dbContext = dbContext;
            _accountService = accountService;
        }

        public async Task<BaseResponse> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var code = await _dbContext.OtpVerifications.Include(v => v.LinkedUser).FirstOrDefaultAsync(v => v.Code == request.Code, cancellationToken);
            if (code is null)
                return new BaseResponse(false, "No user was found");

            var validateOtp = new BaseResponse(false, "");

            switch (request.Purpose)
            {
                case VerificationPurposeEnum.EmailConfirmation:
                    validateOtp = await _accountService.ValidateCodeAsync(new ValidateCodeRequest
                    {
                        Code = request.Code,
                        Purpose = VerificationPurposeEnum.EmailConfirmation,
                        UserId = code.LinkedUser.Id
                    }, cancellationToken);

                    if (!validateOtp.Status)
                        return new BaseResponse(false, validateOtp.Message ?? "Invalid Otp");

                    code.LinkedUser.EmailConfirmed = true;
                    code.LinkedUser.TimeUpdated = DateTimeOffset.UtcNow;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                break;

                case VerificationPurposeEnum.PasswordReset:
                    validateOtp = await _accountService.ValidateCodeAsync(new ValidateCodeRequest
                    {
                        Code = request.Code,
                        Purpose = VerificationPurposeEnum.PasswordReset,
                        UserId = code.LinkedUser.Id
                    }, cancellationToken);

                    if (!validateOtp.Status)
                        return new BaseResponse(false, validateOtp.Message ?? "Invalid Otp");
                break;
            }

            if (request.Purpose == VerificationPurposeEnum.EmailConfirmation)
                return new BaseResponse(true, "Your email address has been successfully verified. You can proceed to login");

            else
                return new BaseResponse(true, "OTP verified successfully. You can now proceed to reset your password.");
        }
    }
}