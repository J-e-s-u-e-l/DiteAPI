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

                    // Persist response
                    var response = new Message
                    {
                        SenderId = userId,
                        MessageBody = request.ResponseMessage,
                        ParentId = request.ParentId,
                        SentAt = DateTimeOffset.UtcNow,
                        IsResponse = true,
                    };

                    await _dbContext.Messages.AddAsync(response);
                    await _dbContext.SaveChangesAsync(cancellationToken);


                    // Broadcast response in real-time
                    var responderDetails = await _dbContext.GenericUser
                        .Where(x => x.Id == userId)
                        .Select(sd => new
                        {
                            responderUsername = sd.UserName,
                            responderRoleInAcademy = sd.AcademyMembersRoles
                                .Select(x => x.IdentityRole.Name).FirstOrDefault()
                        }).FirstOrDefaultAsync();

                    var messageReplyDto = new ResponseDto
                    {
                        ResponseBody = request.ResponseMessage,
                        ResponderUsername = responderDetails.responderUsername,
                        ResponderRoleInAcademy = responderDetails.responderRoleInAcademy,
                        SentAtAgo = _helperMethods.ToAgoFormat(response.SentAt),
                        SentAt = response.SentAt
                    };
                    
                    await _messageBroadcaster.BroadcastReplyAsync(messageReplyDto, request.ParentId);

                    // Send Notification to Message owner
                    var notificationDetails = await _dbContext.Messages
                                                                .Where(x => x.Id == request.ParentId)
                                                                .Select(y => new
                                                                {
                                                                    SenderOfParentMessage = y.SenderId,
                                                                    AcademyName = y.Academy.AcademyName
                                                                }).FirstOrDefaultAsync();

                    var notification = new Notification
                    {
                        UserId = notificationDetails.SenderOfParentMessage,
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

                    await _notificationBroadcaster.BroadcastNotificationAsync(notificationDto, notificationDetails.SenderOfParentMessage.ToString());

                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse(true, "Response has been sent");
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
