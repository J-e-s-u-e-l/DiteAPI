using DiteAPI.Infrastructure.Data.Entities;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface IMessageBroadcaster
    {
        Task BroadcastMessageAsync(Guid id, string messageTitle, string messageBody, Guid? trackId, Guid senderId, DateTimeOffset timeCreated);
    }
}
