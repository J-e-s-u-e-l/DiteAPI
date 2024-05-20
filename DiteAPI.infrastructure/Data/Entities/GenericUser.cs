using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DiteAPI.Models
{
    public class GenericUser : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public Gender UserGender { get; set; }
        public bool IsActive { get; set; }
        public string? signupsessionkey { get; set; }
        public DateTimeOffset LastLogin { get; set; } 
        public DateTimeOffset TimeCreated { get; set; }
        public DateTimeOffset TimeUpdated { get; set; }

        // Navigation properties
        public virtual List<Academy>? Academy{ get; set; }      // 1 user TO MANY academies
    }
}

