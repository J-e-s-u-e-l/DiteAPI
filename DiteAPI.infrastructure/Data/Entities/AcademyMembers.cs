using DiteAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiteAPI.infrastructure.Data.Entities
{
    // Linking Entity between GenericUser and Acadmey to resolve M:M relationship
    public class AcademyMembers : BaseEntity 
    {
#nullable disable
        [Required]
        [ForeignKey(nameof(GenericUser))]
        public Guid GenericUserId { get; set; }

        [Required]
        [ForeignKey(nameof(Academy))]
        public Guid AcademyId { get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual GenericUser GenericUser { get; set; }
        public virtual Academy Academy { get; set; }
    }
}
