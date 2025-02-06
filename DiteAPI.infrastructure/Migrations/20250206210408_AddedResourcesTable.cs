using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedResourcesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourcePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AcademyId",
                table: "Messages",
                column: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Academy_AcademyId",
                table: "Messages",
                column: "AcademyId",
                principalTable: "Academy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Academy_AcademyId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AcademyId",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "F1666B775E192C4C8D8849E58A5A9177", new DateTimeOffset(new DateTime(2025, 1, 27, 8, 59, 40, 145, DateTimeKind.Unspecified).AddTicks(3261), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 27, 8, 59, 40, 145, DateTimeKind.Unspecified).AddTicks(3274), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "49119695BA32634C8E82BE15C6E218C3", new DateTimeOffset(new DateTime(2025, 1, 27, 8, 59, 40, 145, DateTimeKind.Unspecified).AddTicks(3292), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 27, 8, 59, 40, 145, DateTimeKind.Unspecified).AddTicks(3292), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "C668C386CF31DD48A7A1E7286A0CB921", new DateTimeOffset(new DateTime(2025, 1, 27, 8, 59, 40, 145, DateTimeKind.Unspecified).AddTicks(3309), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 27, 8, 59, 40, 145, DateTimeKind.Unspecified).AddTicks(3310), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
