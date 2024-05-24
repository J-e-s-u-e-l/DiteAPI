﻿// <auto-generated />
using System;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    [DbContext(typeof(DataDBContext))]
    [Migration("20240521123430_Added_OtpVerifications")]
    partial class Added_OtpVerifications
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademyGenericUser", b =>
                {
                    b.Property<Guid>("AcademyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenericUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AcademyId", "GenericUserId");

                    b.HasIndex("GenericUserId");

                    b.ToTable("AcademyGenericUser");
                });

            modelBuilder.Entity("DiteAPI.Models.GenericUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastLogin")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("UserGender")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("signupsessionkey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.Academy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AcademyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Academy");
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.AcademyMembers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcademyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenericUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("AcademyId");

                    b.HasIndex("GenericUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AcademyMembers");
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("29877f15-89d9-4962-a960-3a5125104e3e"),
                            ConcurrencyStamp = "0E5A4C353FE7D04F987FFE8B95ACAF60",
                            Description = "",
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            TimeCreated = new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3547), new TimeSpan(0, 0, 0, 0, 0)),
                            TimeUpdated = new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3560), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            Id = new Guid("148e00aa-d441-4d2d-9170-a244cc69b72f"),
                            ConcurrencyStamp = "F5AF6DD4D1487E4298D572A3A13BBD3D",
                            Description = "",
                            Name = "Facilitator",
                            NormalizedName = "FACILITATOR",
                            TimeCreated = new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3574), new TimeSpan(0, 0, 0, 0, 0)),
                            TimeUpdated = new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3575), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            Id = new Guid("94e36e00-e7e9-488c-a0a5-18ff1fcf9887"),
                            ConcurrencyStamp = "73D1509BE02AF24FAAE09F716AA0847F",
                            Description = "",
                            Name = "Member",
                            NormalizedName = "MEMBER",
                            TimeCreated = new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3605), new TimeSpan(0, 0, 0, 0, 0)),
                            TimeUpdated = new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3605), new TimeSpan(0, 0, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcademyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("AcademyId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.OtpVerification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ConfirmedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Purpose")
                        .HasColumnType("int");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipientType")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("OtpVerifications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AcademyGenericUser", b =>
                {
                    b.HasOne("DiteAPI.infrastructure.Data.Entities.Academy", null)
                        .WithMany()
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiteAPI.Models.GenericUser", null)
                        .WithMany()
                        .HasForeignKey("GenericUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.AcademyMembers", b =>
                {
                    b.HasOne("DiteAPI.infrastructure.Data.Entities.Academy", "Academy")
                        .WithMany()
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiteAPI.Models.GenericUser", "GenericUser")
                        .WithMany()
                        .HasForeignKey("GenericUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiteAPI.infrastructure.Data.Entities.ApplicationRole", "IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academy");

                    b.Navigation("GenericUser");

                    b.Navigation("IdentityRole");
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.Course", b =>
                {
                    b.HasOne("DiteAPI.infrastructure.Data.Entities.Academy", "Academy")
                        .WithMany("Courses")
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("DiteAPI.infrastructure.Data.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DiteAPI.Models.GenericUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DiteAPI.Models.GenericUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("DiteAPI.infrastructure.Data.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiteAPI.Models.GenericUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DiteAPI.Models.GenericUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiteAPI.infrastructure.Data.Entities.Academy", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
