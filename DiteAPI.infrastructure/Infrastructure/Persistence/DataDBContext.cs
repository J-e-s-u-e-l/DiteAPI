using DiteAPI.infrastructure.Data.Entities;
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
        public DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Seed();
        }
    }
}
