using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_OtpVerifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "OtpVerifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    ConfirmedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpVerifications", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpVerifications");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("8522f3db-94ad-4853-a7b3-d4798faae500"), "F194F8ADD583334899C9EA68E2DF8672", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5657), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5658), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c45310bf-4c44-4214-894a-7c8a4a143b5f"), "DA43409EB384CC4F8574E7B97A6ADCCC", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5697), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5698), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c5ca2e3a-38f7-41f4-9cdb-30d1ad9f192e"), "B25F7EF0A77A0040BE695C146575E1AC", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5621), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 19, 13, 51, 21, 189, DateTimeKind.Unspecified).AddTicks(5635), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }
    }
}
