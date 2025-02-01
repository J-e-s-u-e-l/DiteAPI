using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<DeleteNotificationCommandHandler> _logger;
        private readonly AppSettings _appSettings;

        public DeleteNotificationCommandHandler(DataDBContext dbContext, ILogger<DeleteNotificationCommandHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var affectedRows = await _dbContext.Notification
                        .Where(n => n.Id == request.NotificationId)
                        .ExecuteDeleteAsync();

                    if (affectedRows == 0)
                    {
                        return new BaseResponse(false, "Notification not found");
                    }

                    return new BaseResponse(true, "Notification deleted");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"DELETE_NOTIFICATION_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"DELETE_NOTIFICATION_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
