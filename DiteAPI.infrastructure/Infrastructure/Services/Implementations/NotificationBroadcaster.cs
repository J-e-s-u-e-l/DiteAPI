
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Infrastructure.Hubs;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Cms;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class NotificationBroadcaster : INotificationBroadcaster
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationBroadcaster(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastNotificationAsync(NotificationDto notificationDto, string recipientId)
        {
            await _hubContext.Clients.Group(recipientId.ToString()).SendAsync("ReceiveNotification", notificationDto);
        }
    }
}
