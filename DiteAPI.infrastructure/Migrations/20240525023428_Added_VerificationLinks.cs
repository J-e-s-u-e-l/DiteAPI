using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_VerificationLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Signupsessionkey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VerificationLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    ConfirmedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationLinks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("0b0c9cd8-e062-4332-973f-808e70019b62"), "C3513BD06C2F8B4483D390DF20BFB00F", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 25, 2, 34, 27, 228, DateTimeKind.Unspecified).AddTicks(71), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 25, 2, 34, 27, 228, DateTimeKind.Unspecified).AddTicks(73), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c051e964-649c-44c6-bda2-94f05fea37d3"), "E212BCD46B5B7646AB865E4E5254325E", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 25, 2, 34, 27, 228, DateTimeKind.Unspecified).AddTicks(38), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 25, 2, 34, 27, 228, DateTimeKind.Unspecified).AddTicks(39), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("cbc6664f-037f-4b21-b3d8-af7be93c13a2"), "0B83B15A5E86F649A71717321BE596CE", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 25, 2, 34, 27, 227, DateTimeKind.Unspecified).AddTicks(9898), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 25, 2, 34, 27, 227, DateTimeKind.Unspecified).AddTicks(9917), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationLinks_UserId",
                table: "VerificationLinks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0b0c9cd8-e062-4332-973f-808e70019b62"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c051e964-649c-44c6-bda2-94f05fea37d3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbc6664f-037f-4b21-b3d8-af7be93c13a2"));

            migrationBuilder.AlterColumn<string>(
                name: "Signupsessionkey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
