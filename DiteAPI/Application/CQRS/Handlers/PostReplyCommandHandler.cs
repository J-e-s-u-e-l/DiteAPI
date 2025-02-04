using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class PostReplyCommandHandler : IRequestHandler<PostReplyCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<PostReplyCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IMessageBroadcaster _messageBroadcaster;
        private readonly INotificationBroadcaster _notificationBroadcaster;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHelperMethods _helperMethods;

        public PostReplyCommandHandler(DataDBContext dbContext, ILogger<PostReplyCommandHandler> logger, IOptions<AppSettings> appSettings, IMessageBroadcaster messageBroadcaster, INotificationBroadcaster notificationBroadcaster, IHttpContextAccessor httpContextAccessor, IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _messageBroadcaster = messageBroadcaster;
            _notificationBroadcaster = notificationBroadcaster;
            _httpContextAccessor = httpContextAccessor;
            _helperMethods = helperMethods;
        }

        public async Task<BaseResponse> Handle(PostReplyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    // Persist reply
                    var reply = new Message
                    {
                        SenderId = userId,
                        MessageBody = request.ReplyBody,
                        SentAt = DateTimeOffset.UtcNow,
                        IsResponse = true,
                    };

                    await _dbContext.Messages.AddAsync(reply);
                    await _dbContext.SaveChangesAsync(cancellationToken);


                    // Broadcast reply in real-time
                    var responderDetails = await _dbContext.GenericUser
                        .Where(x => x.Id == userId)
                        .Select(sd => new
                        {
                            responderUsername = sd.UserName,
                            responderRoleInAcademy = sd.AcademyMembersRoles
                                .Select(x => x.IdentityRole.Name).FirstOrDefault()
                        }).FirstOrDefaultAsync();

                    var messageReplyDto = new MessageReplyDto
                    {
                        ParentId = request.ParentId,
                        ResponseBody = request.ReplyBody,
                        ResponderUsername = responderDetails.responderUsername,
                        ResponderRoleInAcademy = responderDetails.responderRoleInAcademy,
                        SentAt = _helperMethods.ToAgoFormat(reply.SentAt)
                    };
                    
                    await _messageBroadcaster.BroadcastReplyAsync(messageReplyDto);

                    // Send Notification to Message owner
                    var notificationDetails = await _dbContext.Messages
                                                                .Where(x => x.Id == request.ParentId)
                                                                .Select(x => new
                                                                {
                                                                    SenderId = x.SenderId,
                                                                    AcademyName = x.Academy.AcademyName
                                                                }).FirstOrDefaultAsync();

                    var notification = new Notification
                    {
                        UserId = notificationDetails.SenderId,
                        NotificationTitle = notificationDetails.AcademyName,
                        NotificationBody = $"Someone responded to your message"
                    };

                    await _dbContext.Notification.AddAsync(notification);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    // Send real-time notification
                    var notificationDto = new NotificationDto
                    {
                        NotificationId = notification.Id,
                        NotificationTitle = notification.NotificationTitle,
                        NotificationBody = notification.NotificationBody,
                        IsRead = notification.IsRead,
                        TimeStamp = notification.TimeCreated,
                    };

                    await _notificationBroadcaster.BroadcastNotificationAsync(notificationDto, notificationDto.NotificationId.ToString());

                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse(true, "Replay has been sent");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"POST_REPLY_COMMAND_HANDLER  => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"POST_REPLY_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
