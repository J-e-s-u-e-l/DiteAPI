using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DiteAPI.Models
{
    public class GenericUser : IdentityUser<Guid>
    {
        /*[Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }*/
        /*[Required]
        public string MiddleName { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public Gender UserGender { get; set; }*/
        public bool IsActive { get; set; }
        public DateTimeOffset LastLogin { get; set; } 
        public DateTimeOffset TimeCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset TimeUpdated { get; set; } = DateTimeOffset.UtcNow;

        // Navigation properties
        public virtual List<Academy>? Academy{ get; set; }      // 1 user TO MANY academies
        public virtual List<AcademyMembersRoles> AcademyMembersRoles { get; set; }               // 1 GenericUser TO MANY AcademyMembersRoles
    }
}

