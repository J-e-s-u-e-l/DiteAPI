using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_GenericUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "DFC12802EA4B8043B91B6004ACA2BFC8", new DateTimeOffset(new DateTime(2024, 12, 31, 17, 13, 35, 823, DateTimeKind.Unspecified).AddTicks(9047), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 31, 17, 13, 35, 823, DateTimeKind.Unspecified).AddTicks(9059), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "776BB50C9C27FC438A2F6FD8F23FB038", new DateTimeOffset(new DateTime(2024, 12, 31, 17, 13, 35, 823, DateTimeKind.Unspecified).AddTicks(9075), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 31, 17, 13, 35, 823, DateTimeKind.Unspecified).AddTicks(9076), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "1DA7B28A87F6114CBDFEC0619CCA3098", new DateTimeOffset(new DateTime(2024, 12, 31, 17, 13, 35, 823, DateTimeKind.Unspecified).AddTicks(9113), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 31, 17, 13, 35, 823, DateTimeKind.Unspecified).AddTicks(9113), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "8F4A12A6611DA94EA8D6C55A7990CA62", new DateTimeOffset(new DateTime(2024, 12, 23, 21, 10, 6, 411, DateTimeKind.Unspecified).AddTicks(124), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 23, 21, 10, 6, 411, DateTimeKind.Unspecified).AddTicks(139), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "33BDBCAC8C3D704DA7C92D16A69D4DC8", new DateTimeOffset(new DateTime(2024, 12, 23, 21, 10, 6, 411, DateTimeKind.Unspecified).AddTicks(161), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 23, 21, 10, 6, 411, DateTimeKind.Unspecified).AddTicks(163), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "0FF2D991E32E9246BBB98815FCC64F2F", new DateTimeOffset(new DateTime(2024, 12, 23, 21, 10, 6, 411, DateTimeKind.Unspecified).AddTicks(186), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 23, 21, 10, 6, 411, DateTimeKind.Unspecified).AddTicks(187), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
