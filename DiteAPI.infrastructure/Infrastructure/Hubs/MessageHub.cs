using DiteAPI.Infrastructure.Infrastructure.Auth;
using Microsoft.AspNetCore.SignalR;
using System;

namespace DiteAPI.Infrastructure.Infrastructure.Hubs
{
    /*[CustomAuthorize]
    public class DiscussionHub : Hub
    {
        private readonly DataDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DiscussionHub> _logger;
        private readonly AppSettings _appSettings;

        public DiscussionHub(DataDBContext dbContext, IHttpContextAccessor httpContextAccessor, ILogger<DiscussionHub> logger, IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                // Get the list of AcademyIds the user belongs to
                var userAcademies = await _dbContext.AcademyMembers.Where(x => x.GenericUserId == userId).Select(x => x.AcademyId).ToListAsync();

                // Add the user to corresponding SignalR groups
                foreach (var userAcademy in userAcademies)
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Academy-{userAcademy}");

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DISCUSSION_HUB => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                await Clients.Caller.SendAsync("false", _appSettings.ProcessingError);
                return;
            }
        }

        public async Task SendMessage(string academyIdInString, string trackIdInString, string messageTitle, string messageBody)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(messageTitle) || string.IsNullOrWhiteSpace(messageBody) || string.IsNullOrWhiteSpace(academyIdInString) || string.IsNullOrWhiteSpace(trackIdInString))
                {
                    await Clients.Caller.SendAsync("false", "Please fill in all required fields.");
                    return;
                }

                var sentAt = DateTime.UtcNow;

                var senderId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                Guid academyIdInGuid;
                var academyIdToGuid = Guid.TryParse(academyIdInString, out academyIdInGuid);
                if (!academyIdToGuid)
                {
                    await Clients.Caller.SendAsync("false", "Invalid AcademyId format!");
                    return;
                }

                Guid trackIdInGuid;
                string trackName;
                if (Guid.TryParse(trackIdInString, out trackIdInGuid))
                    trackName = (await _dbContext.Tracks.Where(x => x.Id == trackIdInGuid).Select(x => x.TrackName).FirstOrDefaultAsync())!;

                else
                {
                    await Clients.Caller.SendAsync("false", "Invalid TrackId format!");
                    return;
                }

                var senderUserName = _dbContext.GenericUser.Where(x => x.Id == senderId).Select(x => x.UserName).FirstOrDefault();
                //var senderRoleInAcademy = _dbContext.AcademyMembers.Where(x => x.GenericUserId == senderId).Select(x => x.IdentityRole.NormalizedName).FirstOrDefault();
                var responsesToMessage = new List<ResponseToMessage>();

                //Persist message into DB
                var newMessage = new Message
                {
                    MessageTitle = messageTitle,
                    MessageBody = messageBody,
                    AcademyId = academyIdInGuid,
                    TrackId = trackIdInGuid,
                    SenderId = senderId,
                    SentAt = sentAt,
                    IsResponse = false
                };
                await _dbContext.Messages.AddAsync(newMessage);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine(newMessage);

                //await Clients.Group($"Academy-{academyIdInGuid}").SendAsync("ReceiveMessage", messageTitle, messageBody, senderUserName, senderRoleInAcademy, trackName, sentAt, responsesToMessage);
                await Clients.Group($"Academy-{academyIdInGuid}").SendAsync("ReceiveMessage", messageTitle, messageBody, senderUserName, trackName, sentAt, responsesToMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DISCUSSION_HUB => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                await Clients.Caller.SendAsync("false", _appSettings.ProcessingError);
                return;
            }
        }

        public async Task SendMessageResponse(string academyIdInString, string responseBody)
        {
            if (string.IsNullOrWhiteSpace(responseBody))
            {
                await Clients.Caller.SendAsync("false", "Please fill in all required fields.");
                return;
            }

            var sentAt = DateTime.UtcNow;

            var senderId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

            Guid academyIdInGuid;
            var academyIdToGuid = Guid.TryParse(academyIdInString, out academyIdInGuid);
            if (!academyIdToGuid)
            {
                await Clients.Caller.SendAsync("false", "Invalid AcademyId format!");
                return;
            }

            var responderId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

            var responderRoleInAcademy = _dbContext.AcademyMembers.Where(x => x.GenericUserId == responderId).Select(x => x.IdentityRole.NormalizedName).FirstOrDefault();
            var responderUserName = _dbContext.GenericUser.Where(x => x.Id == responderId).Select(x => x.UserName).FirstOrDefault();

            var newResponseToMessage = new Message
            {
                MessageBody = responseBody,
                SenderId = responderId,
                SentAt = sentAt,
                IsResponse = true,
                ParentId = ,
            };
            await _dbContext.Messages.AddAsync(newResponseToMessage);

            await Clients.Group($"Academy-{academyIdInGuid}").SendAsync("ReceiveResponseToMessage", responseBody, responderUserName, responderRoleInAcademy, sentAt); 
        }
    }*/

    [CustomAuthorize]
    public class MessageHub : Hub
    {
        public async Task JoinAcademyGroup(string academyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, academyId);
            //await Console.Out.WriteLineAsync($"User {Context.ConnectionId} joined academy {academyId}");
        }

        public async Task LeaveAcademyGroup(string academyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, academyId);
            //await Console.Out.WriteLineAsync($"User {Context.ConnectionId} left academy {academyId}");
        }
    }

}
