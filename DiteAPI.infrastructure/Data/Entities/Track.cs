using DiteAPI.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class Track : BaseEntity
    {
        public string TrackName { get; set; }

        //FKs
        [Required]
        [ForeignKey(nameof(Academy))] 
        public Guid AcademyId { get; set; }

        // Navigation properties
        public virtual Academy Academy { get; set; }        // MANY courses TO 1 Academy
        public virtual List<AcademyMembersRoles> AcademyMembersRoles { get; set; }               // 1 Track TO MANY AcademyMembersRoles

    }
}
