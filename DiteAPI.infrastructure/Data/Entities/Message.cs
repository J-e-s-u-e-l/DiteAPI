using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Data.Entities
{
    public class Message : BaseEntity
    {
        public string? MessageTitle { get; set; }
        public string MessageBody{ get; set; }
        [ForeignKey(nameof(Academy))]
        public Guid? AcademyId { get; set; }
        [ForeignKey(nameof(Track))]
        public Guid? TrackId { get; set; }
        [ForeignKey(nameof(GenericUser))]
        public Guid SenderId { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsResponse { get; set; }
        [ForeignKey(nameof(Message))]
        public Guid? ParentId { get; set; }
    }
}
