using DiteAPI.infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class Academy : BaseEntity
    {
        public string AcademyName { get; set; }
        public string AcademyCode { get; set; }
        public string? Description{ get; set; }

        [Required]
        [ForeignKey(nameof(GenericUser))]
        public Guid CreatorId { get; set; }

        // Navigation properties
        public virtual List<Track> Tracks { get; set; }               // 1 academy TO MANY tracks
        public virtual List<AcademyMembersRoles> AcademyMembersRoles { get; set; }               // 1 academy TO MANY AcademyMembersRoles
        public virtual List<GenericUser> GenericUser { get; set; }      // 1 academy TO MANY users
    }
}
