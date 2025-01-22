
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class NotificationBroadcaster : INotificationBroadcaster
    {
        public async Task BroadcastNotificationAsync(Guid userId, string message, DateTimeOffset timestamp)
        {
            throw new NotImplementedException();
        }
    }
}
