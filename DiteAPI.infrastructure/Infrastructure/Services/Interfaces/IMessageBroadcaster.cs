using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Data.Entities;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface IMessageBroadcaster
    {
        //Tasks BroadcastMessageAsync(Guid id, string messageTitle, string messageBody, Guid? trackId, Guid senderId, DateTimeOffset timeCreated);
        Task BroadcastMessageAsync(MessageDto messageDto);
        Task BroadcastReplyAsync(ResponseDto replyDto, Guid parentId);
    }
}
