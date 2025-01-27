using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Data.Entities
{
    public class Notification : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(GenericUser))]
        public Guid UserId { get; set; }
        [Required]
        public string NotificationTitle{ get; set; }
        [Required]
        public string NotificationBody { get; set; }
        [Required]
        public bool IsRead { get; set; }

        // Navigation properties
        public virtual GenericUser GenericUser { get; set; }        // 1 USER to MANY Notifications
    }
}
