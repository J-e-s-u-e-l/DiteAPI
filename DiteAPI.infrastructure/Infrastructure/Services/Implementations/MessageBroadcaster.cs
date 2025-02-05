using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Infrastructure.Hubs;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using MimeKit;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class MessageBroadcaster : IMessageBroadcaster
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageBroadcaster(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastMessageAsync(MessageDto messageDto)
        {
            await _hubContext.Clients.Group(messageDto.AcademyId.ToString()).SendAsync("ReceiveMessage", messageDto);
        }

        public async Task BroadcastReplyAsync(ResponseDto replyDto, Guid parentId)
        {
            await _hubContext.Clients.Group(parentId.ToString()).SendAsync("ReceiveReply", replyDto);
        }
    }
}