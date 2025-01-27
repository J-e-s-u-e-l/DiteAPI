using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, BaseResponse<List<GetAllNotificationsResponse>>>
    {

        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllNotificationsQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllNotificationsQueryHandler(DataDBContext dbContext, ILogger<GetAllNotificationsQueryHandler> logger, IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<List<GetAllNotificationsResponse>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext.Items["UserId"];

                    var response = await _dbContext.Notification
                        .Where(n => n.UserId == userId)
                        .OrderByDescending(n => n.TimeCreated)
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(notification => new GetAllNotificationsResponse
                        {
                            NotificationId = notification.Id,
                            NotificationTitle = notification.NotificationTitle,
                            NotificationBody = notification.NotificationBody,
                            IsRead = notification.IsRead,
                            TimeStamp = notification.TimeCreated,
                        })
                        .ToListAsync();

                    return new BaseResponse<List<GetAllNotificationsResponse>>(true, "Notifications retrieved successfully", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"GET_ALL_NOTIFICATIONS_HANDLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<List<GetAllNotificationsResponse>>(false, $"{_appSettings.ProcessingError}");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_NOTIFICATIONS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<List<GetAllNotificationsResponse>>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
