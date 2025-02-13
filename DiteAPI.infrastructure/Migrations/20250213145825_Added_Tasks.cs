using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Tasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CourseTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "FBFBF10C60858644ACA064B40E50BEC0", new DateTimeOffset(new DateTime(2025, 2, 13, 14, 58, 24, 704, DateTimeKind.Unspecified).AddTicks(642), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 13, 14, 58, 24, 704, DateTimeKind.Unspecified).AddTicks(659), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "58DAF026B59FF641A5938977CAA50A6F", new DateTimeOffset(new DateTime(2025, 2, 13, 14, 58, 24, 704, DateTimeKind.Unspecified).AddTicks(674), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 13, 14, 58, 24, 704, DateTimeKind.Unspecified).AddTicks(675), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "9862FB0776C79043AD25C0D22B1A24D8", new DateTimeOffset(new DateTime(2025, 2, 13, 14, 58, 24, 704, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 13, 14, 58, 24, 704, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "3956457F75CA374DB75B7B940837FDA3", new DateTimeOffset(new DateTime(2025, 2, 6, 21, 4, 7, 315, DateTimeKind.Unspecified).AddTicks(1064), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 6, 21, 4, 7, 315, DateTimeKind.Unspecified).AddTicks(1077), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "B35E928292A7B0439961DA998E580B75", new DateTimeOffset(new DateTime(2025, 2, 6, 21, 4, 7, 315, DateTimeKind.Unspecified).AddTicks(1108), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 6, 21, 4, 7, 315, DateTimeKind.Unspecified).AddTicks(1109), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "09D51724B833C74C97AF8F054B564011", new DateTimeOffset(new DateTime(2025, 2, 6, 21, 4, 7, 315, DateTimeKind.Unspecified).AddTicks(1137), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 6, 21, 4, 7, 315, DateTimeKind.Unspecified).AddTicks(1138), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
