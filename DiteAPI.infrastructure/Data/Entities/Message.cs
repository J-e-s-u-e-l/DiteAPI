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
        /* 
         On creating a record in the on the Message table:
         TrackId & AcademyId can be left null IF AND ONLY IF the message is a response to another message within the same academy
         ParentId can be left null IF AND ONLY IF the message is not a response to another message within the same academy
        */
        public string? MessageTitle { get; set; }

        public string MessageBody{ get; set; }

        [ForeignKey(nameof(Academy))]
        public Guid? AcademyId { get; set; }

        [ForeignKey(nameof(Track))]
        public Guid? TrackId { get; set; }
        //public Guid TrackId { get; set; } 

        [ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public bool IsResponse { get; set; }

        [ForeignKey(nameof(Message))]
        public Guid? ParentId { get; set; }

        // Navigation properties
        public virtual GenericUser Sender { get; set; }     // I MESSAGE to 1 USER
        public virtual Track Track { get; set; }     // I MESSAGE to 1 TRACK
    }
}
