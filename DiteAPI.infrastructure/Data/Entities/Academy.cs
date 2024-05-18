using DiteAPI.infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class Academy : BaseEntity
    {
        public string AcademyName { get; set; }
        public string AcademyCode { get; set; } = new HelperMethods().GenerateUniqueString();

        public string? Description{ get; set; }

        // FKs
        [Required]
        [ForeignKey(nameof(GenericUser))]
        public Guid CreatorId { get; set; }

        // Navigation properties
        public virtual List<Course> Courses { get; set; }               // 1 academy TO MANY courses
        public virtual List<GenericUser> GenericUser { get; set; }      // 1 academy TO MANY users
    }
}
