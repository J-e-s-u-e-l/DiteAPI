using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class PostMessageCommandHandler : IRequestHandler<PostMessageCommand, BaseResponse<PostMessageResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<PostMessageCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IMessageBroadcaster _messageBroadcaster;
        private readonly INotificationBroadcaster _notificationBroadcaster;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostMessageCommandHandler(DataDBContext dbContext, ILogger<PostMessageCommandHandler> logger, IOptions<AppSettings> options, INotificationBroadcaster notificationBroadcaster, IMessageBroadcaster messageBroadcaster, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _notificationBroadcaster = notificationBroadcaster;
            _messageBroadcaster = messageBroadcaster;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<PostMessageResponse>> Handle(PostMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    // Persist the message in the database
                    var message = new Message
                    {
                        MessageTitle = request.MessageTitle,
                        MessageBody = request.MessageBody,
                        AcademyId = request.AcademyId,
                        TrackId = request.TrackId,
                        SenderId = userId,
                        SentAt = DateTimeOffset.UtcNow,
                        IsResponse = false,
                    };

                    await _dbContext.Messages.AddAsync(message);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    // Broadcast the message in real-time
                    //var senderDetails = await _dbContext.GenericUser.FirstOrDefaultAsync(x => x.Id == userId);
                    var senderDetails = await _dbContext.GenericUser
                        .Where(x => x.Id == userId)
                        .Select(sd => new
                        {
                            senderUsername = sd.UserName,
                            senderRoleInAcademy = sd.AcademyMembersRoles
                                //.Where(x => x.GenericUserId == userId)
                                .Select(x => x.IdentityRole.Name).FirstOrDefault()
                        }).FirstOrDefaultAsync();

                    var messageDto = new MessageDto
                    {
                        MessageTitle = request.MessageTitle,
                        MessageBody = request.MessageBody,
                        SenderUserName = senderDetails.senderUsername,
                        SenderRoleInAcademy = senderDetails.senderRoleInAcademy,
                        TrackName = await _dbContext.Tracks.Where(t => t.Id == request.TrackId).Select(t => t.TrackName).FirstOrDefaultAsync(),
                        SentAt = message.SentAt                    
                    };
                    await _messageBroadcaster.BroadcastMessageAsync(message.Id, message.MessageTitle, message.MessageBody, message.TrackId, message.SenderId, message.TimeCreated);
                    //await _messageBroadcaster.BroadcastMessageAsync(message);

                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"POST_MESSAGE_COMMAND_HANDLER  => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse<PostMessageResponse>(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"POST_MESSAGE_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<PostMessageResponse>(false, _appSettings.ProcessingError);
            }
        }
    }
}
