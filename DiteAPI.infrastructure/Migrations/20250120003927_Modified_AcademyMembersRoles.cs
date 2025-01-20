using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_AcademyMembersRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TrackId",
                table: "AcademyMembersRoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "7FBDFC9F0BE1474992664EC36681B1C6", new DateTimeOffset(new DateTime(2025, 1, 20, 0, 39, 26, 282, DateTimeKind.Unspecified).AddTicks(8615), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 20, 0, 39, 26, 282, DateTimeKind.Unspecified).AddTicks(8631), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "16CC6C2409D4774EA4C37C63326AA57C", new DateTimeOffset(new DateTime(2025, 1, 20, 0, 39, 26, 282, DateTimeKind.Unspecified).AddTicks(8650), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 20, 0, 39, 26, 282, DateTimeKind.Unspecified).AddTicks(8651), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "042720565EC7CD48902B77F0D6EEDEEC", new DateTimeOffset(new DateTime(2025, 1, 20, 0, 39, 26, 282, DateTimeKind.Unspecified).AddTicks(8666), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 20, 0, 39, 26, 282, DateTimeKind.Unspecified).AddTicks(8667), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TrackId",
                table: "AcademyMembersRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "BFC0A343BB1D214FA861825D374BA3F5", new DateTimeOffset(new DateTime(2025, 1, 19, 22, 57, 30, 745, DateTimeKind.Unspecified).AddTicks(6870), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 22, 57, 30, 745, DateTimeKind.Unspecified).AddTicks(6880), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "0BF6B97D670B6E4EA621A11CB709368E", new DateTimeOffset(new DateTime(2025, 1, 19, 22, 57, 30, 745, DateTimeKind.Unspecified).AddTicks(6894), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 22, 57, 30, 745, DateTimeKind.Unspecified).AddTicks(6895), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "0F3367DB3552464F8D792DE28252AAF8", new DateTimeOffset(new DateTime(2025, 1, 19, 22, 57, 30, 745, DateTimeKind.Unspecified).AddTicks(6968), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 22, 57, 30, 745, DateTimeKind.Unspecified).AddTicks(6969), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
