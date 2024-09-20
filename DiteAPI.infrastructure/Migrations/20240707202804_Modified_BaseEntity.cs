using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_BaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ae989e1-c1f1-43fc-8c64-1b5ed33c0cca"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f861559e-30e9-4947-8b9a-a70237677b07"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb5fbbeb-77ae-4ca1-8bb5-7805ecab0de3"));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "VerificationTokens",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "VerificationTokens",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "Tracks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "Tracks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "OtpVerifications",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "OtpVerifications",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "Messages",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "Messages",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "AcademyMembers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "AcademyMembers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "Academy",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "Academy",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "C106703227DD33478DC88D3C5EA7E182", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 7, 7, 20, 28, 3, 167, DateTimeKind.Unspecified).AddTicks(2265), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 20, 28, 3, 167, DateTimeKind.Unspecified).AddTicks(2291), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "E247F32B3A2AE648A433285789F3B2FC", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 7, 7, 20, 28, 3, 167, DateTimeKind.Unspecified).AddTicks(2342), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 20, 28, 3, 167, DateTimeKind.Unspecified).AddTicks(2345), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "F38B94CDBB23DA4B9B51D8D9AE11099D", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 7, 7, 20, 28, 3, 167, DateTimeKind.Unspecified).AddTicks(2397), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 20, 28, 3, 167, DateTimeKind.Unspecified).AddTicks(2400), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "VerificationTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "VerificationTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "OtpVerifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "OtpVerifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "AcademyMembers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AcademyMembers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "Academy",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Academy",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("1ae989e1-c1f1-43fc-8c64-1b5ed33c0cca"), "C7CCDC3E4EFB6542A250E3336F96A8B5", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2878), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2889), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f861559e-30e9-4947-8b9a-a70237677b07"), "A386B15A5CDC4D47927BBB52A42816FF", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2974), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2975), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("fb5fbbeb-77ae-4ca1-8bb5-7805ecab0de3"), "88021A5DDAC2F847B622BB378BE659DF", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2906), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2908), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
