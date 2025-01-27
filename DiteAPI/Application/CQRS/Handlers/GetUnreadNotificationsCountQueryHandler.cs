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
    public class GetUnreadNotificationsCountQueryHandler : IRequestHandler<GetUnreadNotificationsCountQuery, BaseResponse<GetUnreadNotificationsCountResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetUnreadNotificationsCountQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUnreadNotificationsCountQueryHandler(DataDBContext dbContext, ILogger<GetUnreadNotificationsCountQueryHandler> logger, IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<GetUnreadNotificationsCountResponse>> Handle(GetUnreadNotificationsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                var notificationCount = await _dbContext.Notification.Where(n => n.IsRead == false).CountAsync();

                return new BaseResponse<GetUnreadNotificationsCountResponse>(true, "Unread notifications count retrieved sucessfully", new GetUnreadNotificationsCountResponse { UnreadNotificationsCount = notificationCount });
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_MEMBERS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetUnreadNotificationsCountResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
