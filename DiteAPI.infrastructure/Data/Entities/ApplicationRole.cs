using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using DiteAPI.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        public DateTimeOffset TimeCreated { get; set; }
        public DateTimeOffset TimeUpdated { get; set; }

        // Navigation Properties
        public virtual List<AcademyMembersRoles> AcademyMembersRoles { get; set; }               // 1 academy TO MANY AcademyMembersRoles

    }
}
