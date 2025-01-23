
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Infrastructure.Hubs;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class NotificationBroadcaster : INotificationBroadcaster
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationBroadcaster(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastNotificationAsync(NotificationDto notificationDto)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationDto);
        }
    }
}
