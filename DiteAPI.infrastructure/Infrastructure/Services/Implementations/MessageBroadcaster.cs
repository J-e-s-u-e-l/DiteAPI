using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Infrastructure.Hubs;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class MessageBroadcaster : IMessageBroadcaster
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageBroadcaster(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

       /* public async Task BroadcastMessageAsync(Message message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }*/

        public async Task BroadcastMessageAsync(Guid id, string messageTitle, string messageBody, Guid? trackId, Guid senderId, DateTimeOffset timeCreated)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", id, messageTitle, messageBody, trackId, senderId, timeCreated);
        }
    }
}
