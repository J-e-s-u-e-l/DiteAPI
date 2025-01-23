using DiteAPI.infrastructure.Data.Models;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface INotificationBroadcaster
    {
        //Task BroadcastNotificationAsync(Guid userId, string message, DateTimeOffset timestamp);
        Task BroadcastNotificationAsync(NotificationDto notificationDto);
    }
}
