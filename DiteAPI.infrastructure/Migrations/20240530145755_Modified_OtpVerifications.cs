using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_OtpVerifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5eedd614-9613-4a5b-b51e-ce93774b7854"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6fd83264-c892-4f23-be65-ffb85966633a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dd788770-a848-4005-b883-686bbcb622dd"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("1b3fd20b-1a8d-4bdd-9227-801b319b1256"), "9BFB0FDFAADE7644B954A552D21A5AA0", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2841), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2843), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("abf5d03a-d4f2-4f50-9888-bdeff993368d"), "6B734209E75C8D488B3D81C2FAB5B25A", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2758), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2772), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c86458cd-8053-4999-afd6-69b8f17e7205"), "5B2EDE6B665317419DB7BC7E6A7D422E", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2807), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 14, 57, 54, 976, DateTimeKind.Unspecified).AddTicks(2809), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtpVerifications_UserId",
                table: "OtpVerifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpVerifications_AspNetUsers_UserId",
                table: "OtpVerifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtpVerifications_AspNetUsers_UserId",
                table: "OtpVerifications");

            migrationBuilder.DropIndex(
                name: "IX_OtpVerifications_UserId",
                table: "OtpVerifications");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("5eedd614-9613-4a5b-b51e-ce93774b7854"), "C0E960483C1E634D9DDB37C8664D9248", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8006), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8007), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6fd83264-c892-4f23-be65-ffb85966633a"), "912B08533926314487EFCB4855A217C6", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8030), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8031), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("dd788770-a848-4005-b883-686bbcb622dd"), "77C7EF7542363B4D864A5E29E73CB898", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(7966), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(7978), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
