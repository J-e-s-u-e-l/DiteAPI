using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_GenericUser_RemovedSignUpSessionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b3fd20b-1a8d-4bdd-9227-801b319b1256"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("abf5d03a-d4f2-4f50-9888-bdeff993368d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c86458cd-8053-4999-afd6-69b8f17e7205"));

            migrationBuilder.DropColumn(
                name: "Signupsessionkey",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("00be60d8-2b94-4090-a582-c6e280613572"), "C808B0E8BAE96D49ABD33F7091CAAD85", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1846), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1847), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("01919842-2a75-4788-9051-9a88ba7f6a6a"), "4C2E5F46B6EC154F9C89C2B772CECB61", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1715), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1726), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("69de252e-bcda-4e37-916a-439106707b88"), "2929FE36329C9643A95C3AD792C96226", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1740), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1741), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00be60d8-2b94-4090-a582-c6e280613572"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("01919842-2a75-4788-9051-9a88ba7f6a6a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("69de252e-bcda-4e37-916a-439106707b88"));

            migrationBuilder.AddColumn<string>(
                name: "Signupsessionkey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("1b3fd20b-1a8d-4bdd-9227-801b319b1256"), "9BFB0FDFAADE7644B954A552D21A5AA0", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2841), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2843), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("abf5d03a-d4f2-4f50-9888-bdeff993368d"), "6B734209E75C8D488B3D81C2FAB5B25A", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2758), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2772), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c86458cd-8053-4999-afd6-69b8f17e7205"), "5B2EDE6B665317419DB7BC7E6A7D422E", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2807), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2809), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
