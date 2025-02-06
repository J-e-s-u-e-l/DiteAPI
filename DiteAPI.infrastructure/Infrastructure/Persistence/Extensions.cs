using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Implementations;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ISessionService, SessionService>();
            services.AddSignalR();
            services.AddScoped<IMessageBroadcaster, MessageBroadcaster>();
            services.AddScoped<INotificationBroadcaster, NotificationBroadcaster>();
            services.AddScoped<IAcademyRepository, AcademyRepository>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }

        public static IServiceCollection RegisterCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                string[] origins = { "http://127.0.0.1:5500", "http://localhost:3000" };

                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                    builder.WithOrigins(origins);
                });
            });

            return services;
        }

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
            services.AddOptions<ContactInformation>();
            services.Configure<ContactInformation>(configuration.GetSection("ContactInformation"));
            return services;
        }

        public static IServiceCollection RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<AppSettings>();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

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

        public static void SetUp(this ModelBuilder modelBuilder)
        {
            #region Seed User Roles
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = GenerateCode(),
                    Description = "",
                    TimeCreated = DateTime.UtcNow,
                    TimeUpdated = DateTime.UtcNow
                },
                new ApplicationRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Facilitator",
                    NormalizedName = "FACILITATOR",
                    ConcurrencyStamp = GenerateCode(),
                    Description = "",
                    TimeCreated = DateTime.UtcNow,
                    TimeUpdated = DateTime.UtcNow
                },
                new ApplicationRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"), 
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = GenerateCode(),
                    Description = "",
                    TimeCreated = DateTime.UtcNow,
                    TimeUpdated = DateTime.UtcNow
                }
            );
            #endregion

            #region Entity Delete Behaviour
            /*modelBuilder.Entity<AcademyMembersRoles>(entity =>
            {
                // Prevent cascading delete for Academy
                entity.HasOne(amr => amr.Academy)
                .WithMany(a => a.AcademyMembersRoles)
                .HasForeignKey(amr => amr.AcademyId)
                .OnDelete(DeleteBehavior.Restrict);

                // Prevent cascading delete for GenericUser
                entity.HasOne(amr => amr.GenericUser)
                .WithMany(gu => gu.AcademyMembersRoles)
                .HasForeignKey(amr => amr.GenericUserId)
                .OnDelete(DeleteBehavior.Restrict);

                // Prevent cascading delete for TrackName
                entity.HasOne(amr => amr.TrackName)
                .WithMany(ir => ir.AcademyMembersRoles)
                .HasForeignKey(amr => amr.TrackId)
                .OnDelete(DeleteBehavior.Restrict);

                // Prevent cascading delete for IdentityRole
                entity.HasOne(amr => amr.IdentityRole)
                .WithMany(ir => ir.AcademyMembersRoles)
                .HasForeignKey(amr => amr.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            });*/
            #endregion

            /*#region Unique UserName
            modelBuilder.Entity<GenericUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            #endregion
            
            #region Unique Email
            modelBuilder.Entity<GenericUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
            #endregion*/
        }

        private static readonly Func<string> GenerateCode = () =>
        {
            var guid = Guid.NewGuid();
            return string.Concat(Array.ConvertAll(guid.ToByteArray(), b => b.ToString("X2")));
        };

    }
}
