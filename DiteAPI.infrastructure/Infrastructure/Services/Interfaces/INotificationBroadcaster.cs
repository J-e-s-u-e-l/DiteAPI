namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface INotificationBroadcaster
    {
        Task BroadcastNotificationAsync(Guid userId, string message, DateTimeOffset timestamp);
    }
}
