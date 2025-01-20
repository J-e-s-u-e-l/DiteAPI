using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiteAPI.Infrastructure.Data.Entities
{
    // This Entity contains the roles of all the members which resolves the M : M relationship as MANY users can have MANY Roles within the same academy
    public class AcademyMembersRoles : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(GenericUser))]
        public Guid GenericUserId { get; set; }
            
        [Required]
        [ForeignKey(nameof(Academy))]
        public Guid AcademyId { get; set; }

        [ForeignKey(nameof(Track))]
        public Guid? TrackId { get; set; }

        [Required]
        [ForeignKey(nameof(IdentityRole))]
        public Guid RoleId { get; set; }

        // Navigation properties
        //public virtual List<Academy> Academy { get; set; }
        public virtual Academy Academy { get; set; }   
        public virtual GenericUser GenericUser { get; set; }
        public virtual Track Track { get; set; }
        public virtual ApplicationRole IdentityRole { get; set; }
    }
}
