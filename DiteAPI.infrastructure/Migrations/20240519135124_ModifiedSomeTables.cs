using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedSomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "signupsessionkey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("8522f3db-94ad-4853-a7b3-d4798faae500"), "F194F8ADD583334899C9EA68E2DF8672", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5657), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5658), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c45310bf-4c44-4214-894a-7c8a4a143b5f"), "DA43409EB384CC4F8574E7B97A6ADCCC", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5697), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5698), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c5ca2e3a-38f7-41f4-9cdb-30d1ad9f192e"), "B25F7EF0A77A0040BE695C146575E1AC", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5621), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5635), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8522f3db-94ad-4853-a7b3-d4798faae500"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c45310bf-4c44-4214-894a-7c8a4a143b5f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c5ca2e3a-38f7-41f4-9cdb-30d1ad9f192e"));

            migrationBuilder.DropColumn(
                name: "signupsessionkey",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("4269470b-eeb2-403b-9357-981c35d4b340"), "08AA676F2B40764093C7A4531172790E", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2242), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2243), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("54936eee-ca99-4e2b-b60b-8a129d3cd298"), "6BF23851C2B9B84E919F8EEE9E60B471", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2204), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2218), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("74b04a72-58b5-47c5-845a-9f0b28d8bf50"), "6AE95E83F536644E9C344570B2B62EFC", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2265), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 22, 3, 45, 67, DateTimeKind.Unspecified).AddTicks(2266), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
