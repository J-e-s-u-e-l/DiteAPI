using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<MarkNotificationAsReadCommandHandler> _logger;
        private readonly AppSettings _appSettings;

        public MarkNotificationAsReadCommandHandler(DataDBContext dbContext, ILogger<MarkNotificationAsReadCommandHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await _dbContext.Notification
                        .Where(n => n.Id == request.NotificationId)
                        .ExecuteUpdateAsync(update => update
                        .SetProperty(n => n.IsRead, true));

                    await transaction.CommitAsync();

                    return new BaseResponse(true, "Notification marked as read");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"MARK_NOTIFICATION_AS_READ_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"MARK_NOTIFICATION_AS_READ_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
