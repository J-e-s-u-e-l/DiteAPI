using DiteAPI.Infrastructure.Infrastructure.Auth;
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
    }
}
