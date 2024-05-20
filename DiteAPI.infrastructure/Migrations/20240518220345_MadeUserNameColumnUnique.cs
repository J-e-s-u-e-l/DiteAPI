using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeUserNameColumnUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("471fa4ed-0833-4865-bb13-57397333bfdc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("86fc2726-2b41-4b3c-aed5-93c273115054"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8c9092b-f35f-49eb-bcff-034400dfb976"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("4269470b-eeb2-403b-9357-981c35d4b340"), "08AA676F2B40764093C7A4531172790E", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2242), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2243), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("54936eee-ca99-4e2b-b60b-8a129d3cd298"), "6BF23851C2B9B84E919F8EEE9E60B471", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2204), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2218), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("74b04a72-58b5-47c5-845a-9f0b28d8bf50"), "6AE95E83F536644E9C344570B2B62EFC", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2265), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2266), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4269470b-eeb2-403b-9357-981c35d4b340"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("54936eee-ca99-4e2b-b60b-8a129d3cd298"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("74b04a72-58b5-47c5-845a-9f0b28d8bf50"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("471fa4ed-0833-4865-bb13-57397333bfdc"), "B56B2D183436064F9EB0F945B306DEF1", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 17, 9, 19, 8, 333, DateTimeKind.Unspecified).AddTicks(1007), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 17, 9, 19, 8, 333, DateTimeKind.Unspecified).AddTicks(1010), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("86fc2726-2b41-4b3c-aed5-93c273115054"), "07CADC4285B26B4891E44441E95C3415", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 17, 9, 19, 8, 333, DateTimeKind.Unspecified).AddTicks(947), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 17, 9, 19, 8, 333, DateTimeKind.Unspecified).AddTicks(961), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e8c9092b-f35f-49eb-bcff-034400dfb976"), "370C83A5296D2C4399DB0128C9B7160B", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 17, 9, 19, 8, 333, DateTimeKind.Unspecified).AddTicks(1072), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 17, 9, 19, 8, 333, DateTimeKind.Unspecified).AddTicks(1075), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
