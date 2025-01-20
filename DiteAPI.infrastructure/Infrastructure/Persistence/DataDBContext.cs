using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.infrastructure.Infrastructure.Persistence
{
    public class DataDBContext : IdentityDbContext<GenericUser, ApplicationRole, Guid>
    //public class DataDBContext : IdentityDbContext<GenericUser>
    {
        public DataDBContext(DbContextOptions<DataDBContext> options) : base(options) { }

        public DbSet<GenericUser> GenericUser { get; set; }
        public DbSet<Academy> Academy { get; set; } 
        public DbSet<AcademyMembers> AcademyMembers { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<OtpVerification> OtpVerifications { get; set; }
        public DbSet<VerificationTokens> VerificationTokens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AcademyMembersRoles> AcademyMembersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.SetUp();
        }
    }
}
