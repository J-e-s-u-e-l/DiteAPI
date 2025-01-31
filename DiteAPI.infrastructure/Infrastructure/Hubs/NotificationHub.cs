using DiteAPI.Infrastructure.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Hubs
{
    [CustomAuthorize]
    public class NotificationHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.Items["UserId"] is string userId && !string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_httpContextAccessor.HttpContext?.Items["UserId"] is string userId)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        //public async Task JoinNotificationGroup(string userId)
        /*public async Task JoinNotificationGroup()
        {
            //var userId = _httpContextAccessor.HttpContext!.Items["UserId"].ToString();

            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Items["UserId"].ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }*/

        /*public async Task LeaveNotificationGroup()
        {
            var userId = _httpContextAccessor.HttpContext.Items["UserId"].ToString();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }*/
    }
}
