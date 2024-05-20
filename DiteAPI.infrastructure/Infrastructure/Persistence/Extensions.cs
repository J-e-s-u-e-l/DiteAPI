using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiteAPI.infrastructure.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region User Roles
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = GenerateCode(),
                    Description = "",
                    TimeCreated = DateTime.UtcNow,
                    TimeUpdated = DateTime.UtcNow
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Facilitator",
                    NormalizedName = "FACILITATOR",
                    ConcurrencyStamp = GenerateCode(),
                    Description = "",
                    TimeCreated = DateTime.UtcNow,
                    TimeUpdated = DateTime.UtcNow
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = GenerateCode(),
                    Description = "",
                    TimeCreated = DateTime.UtcNow,
                    TimeUpdated = DateTime.UtcNow
                }
                );
            #endregion

            #region Unique UserName
            modelBuilder.Entity<GenericUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            #endregion
            
            #region Unique Email
            modelBuilder.Entity<GenericUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
            #endregion
        }

        private static readonly Func<string> GenerateCode = () =>
        {
            var guid = Guid.NewGuid();
            return string.Concat(Array.ConvertAll(guid.ToByteArray(), b => b.ToString("X2")));
        };

    }
}
