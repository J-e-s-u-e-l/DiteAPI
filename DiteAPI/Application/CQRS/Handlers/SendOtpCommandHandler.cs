using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<SendOtpCommandHandler> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly ISessionService _sessionService;
        private readonly AppSettings _appSettings;

        public SendOtpCommandHandler(DataDBContext dbContext, ILogger<SendOtpCommandHandler> logger, IConfiguration configuration, IAccountService accountService, ISessionService sessionService, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
            _accountService = accountService;
            _sessionService = sessionService;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(SendOtpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    if (request.RecipientType == OtpRecipientTypeEnum.Email)
                    {
                        var user = await _dbContext.GenericUser.FirstOrDefaultAsync(x => x.Email == request.Recipient);
/*
                        if (user == null)
                            return new BaseResponse(false, _appSettings.UserWithEmailNotFound);
*/
                        // Store user details in session
                        _sessionService.SetStringInSession("UserId", user.Id.ToString());
                        _sessionService.SetStringInSession("IsOtpVerified", "false");

                        var sendEmail = await _accountService.SendOTPAsync(new SendOTPToUser
                        { 
                            FirstName = user.UserName,
                            OtpCodeLength = OtpCodeLengthEnum.Six,
                            Purpose = request.Purpose,
                            Recipient = user.Email!,
                            RecipientType = OtpRecipientTypeEnum.Email,
                            UserId = user.Id
                        }, cancellationToken);
                        if (!sendEmail.Status)
                        {
                            await transaction.RollbackAsync(cancellationToken);
                            return new BaseResponse(false, sendEmail.Message!);
                        }
                    }
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse(true, $"OTP has been sent to {request.Recipient}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"SEND_OTP_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    return new BaseResponse(false, $"{_appSettings.ProcessingError}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"SEND_OTP_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
