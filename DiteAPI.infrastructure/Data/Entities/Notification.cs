using DiteAPI.infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Data.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string NotificationTitle{ get; set; }
        public string NotificationBody { get; set; }
        public bool IsRead { get; set; }
    }
}
