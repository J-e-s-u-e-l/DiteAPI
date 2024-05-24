using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedColumn_In_GenericUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("148e00aa-d441-4d2d-9170-a244cc69b72f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("29877f15-89d9-4962-a960-3a5125104e3e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("94e36e00-e7e9-488c-a0a5-18ff1fcf9887"));

            migrationBuilder.RenameColumn(
                name: "signupsessionkey",
                table: "AspNetUsers",
                newName: "Signupsessionkey");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("317a8e1c-268d-4c65-b621-3a535a5af550"), "C70540E600D0AA4CBC2D82BD404B66AF", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 21, 20, 52, 5, 392, DateTimeKind.Unspecified).AddTicks(4622), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 21, 20, 52, 5, 392, DateTimeKind.Unspecified).AddTicks(4623), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("4e5d7cbd-6a56-4855-86c1-63edce0e1211"), "75CC618669C80F46B20F25B82D057E7E", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 21, 20, 52, 5, 392, DateTimeKind.Unspecified).AddTicks(4584), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 21, 20, 52, 5, 392, DateTimeKind.Unspecified).AddTicks(4585), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("b343bfb5-dd75-4232-89f9-b5ab61e6ee03"), "B0E16D00042720459C2F9A467289D3A5", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 21, 20, 52, 5, 392, DateTimeKind.Unspecified).AddTicks(4547), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 21, 20, 52, 5, 392, DateTimeKind.Unspecified).AddTicks(4565), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("317a8e1c-268d-4c65-b621-3a535a5af550"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e5d7cbd-6a56-4855-86c1-63edce0e1211"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b343bfb5-dd75-4232-89f9-b5ab61e6ee03"));

            migrationBuilder.RenameColumn(
                name: "Signupsessionkey",
                table: "AspNetUsers",
                newName: "signupsessionkey");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("148e00aa-d441-4d2d-9170-a244cc69b72f"), "F5AF6DD4D1487E4298D572A3A13BBD3D", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3574), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3575), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("29877f15-89d9-4962-a960-3a5125104e3e"), "0E5A4C353FE7D04F987FFE8B95ACAF60", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3547), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3560), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("94e36e00-e7e9-488c-a0a5-18ff1fcf9887"), "73D1509BE02AF24FAAE09F716AA0847F", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3605), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 21, 12, 34, 29, 603, DateTimeKind.Unspecified).AddTicks(3605), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
