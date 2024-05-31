using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;

namespace DiteAPI.infrastructure.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMailkitService, MailkitService>();
            services.AddScoped<IHelperMethods, HelperMethods>();

            return services;
        }

        /*public static IServiceCollection RegisterCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                var origin = "https://j-e-s-u-e-l.github.io/DiteFrontend/";

                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                    builder.WithOrigins(origin);
                });
            });

            return services;
        }*/

        public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection RegisterContactInformation(this IServiceCollection services, IConfiguration configuration)
        {
            var contactInfo = new ContactInformation();
            configuration.Bind("ContactInformation", contactInfo);
            services.AddSingleton(contactInfo);

            //services.Configure<ContactInformation>(configuration.GetSection("ContactInformation"));
            return services;
        }
        
        public static IServiceCollection RegisterMailKitSection(this IServiceCollection services, IConfiguration configuration)
        {
            var mailKitSection = new MailKitSection();
            configuration.Bind("MailKitSection", mailKitSection);
            services.AddSingleton(mailKitSection);

            //services.Configure<MailKitSection>(configuration.GetSection("MailKitSection"));
            return services;
        }
        
        public static IServiceCollection RegisterEmailVerification(this IServiceCollection services, IConfiguration configuration)
        {
            var verification = new Verification();
            configuration.Bind("Verification", verification);
            services.AddSingleton(verification);

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
