using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class ResetPasswordCommandHander : IRequestHandler<ResetPasswordCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ISessionService _sessionService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<ResetPasswordCommand> _logger;
        private UserManager<GenericUser> _userManager; 

        public ResetPasswordCommandHander(DataDBContext dbContext, ISessionService sessionService, IOptions<AppSettings> options, UserManager<GenericUser> userManager, ILogger<ResetPasswordCommand> logger)
        {
            _dbContext = dbContext;
            _sessionService = sessionService;
            _appSettings = options.Value;
            _userManager = userManager;
            _logger = logger;

        }
        public async Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    if (_sessionService.GetStringFromSession("IsOtpVerified") == "false")
                        return new BaseResponse(false, _appSettings.OtpNotVerified);

                    var userId = _sessionService.GetStringFromSession("UserId");
                    if(userId == null)
                        return new BaseResponse(false, _appSettings.NoActiveSession);


                    var user = _dbContext.GenericUser.FirstOrDefault(x => x.Id.Equals(Guid.Parse(userId)));

                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetToken, request.ConfirmNewPassword);

                    if (!resetPasswordResult.Succeeded)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new BaseResponse(false, _appSettings.ProcessingError);
                    }

                    // reset lockout count
                    user.LockoutEnabled = false;
                    user.AccessFailedCount = 0;
                    user.LockoutEnd = DateTimeOffset.UtcNow;

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return new BaseResponse(true, _appSettings.PasswordResetSuccessful);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"RESETPASSWORD_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"RESETPASSWORD_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError); 
            }
        }
    }
}
