using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
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
    public class VerifyAccountOtpRequestHandler : IRequestHandler<VerifyAccountOtpRequest, BaseResponse>
    {
            private readonly DataDBContext _dbContext;
            private readonly IAccountService _accountService;

            public VerifyAccountOtpRequestHandler(DataDBContext dbContext, UserManager<GenericUser> userManager, ILogger<Registration> logger, IAccountService accountService, IEmailService emailService)
            {
                _dbContext = dbContext;
                _accountService = accountService;
            }

        public async Task<BaseResponse> Handle(VerifyAccountOtpRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.GenericUser.FirstOrDefaultAsync(x => x.Signupsessionkey == request.Signupsessionkey, cancellationToken);
            if (user is null)
                return new BaseResponse(false, "No user was found");

            var validateOtp = await _accountService.ValidateOTPCodeAsync(new ValidateOtpRequest
            {
                Code = request.Code,
                Purpose = OtpVerificationPurposeEnum.EmailConfirmation,
                UserId = user.Id
            }, cancellationToken);
            if (!validateOtp.Status)
                return new BaseResponse(false, validateOtp.Message ?? "Invalid Otp");

            user.EmailConfirmed = true;
            user.TimeUpdated = DateTimeOffset.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new BaseResponse(true, "Your email address has been successfully verified. You can proceed to login");
        }
    }
}

