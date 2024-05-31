using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_VerificationTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "VerificationTokens",
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
                    table.PrimaryKey("PK_VerificationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationTokens_AspNetUsers_UserId",
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
                    { new Guid("5eedd614-9613-4a5b-b51e-ce93774b7854"), "C0E960483C1E634D9DDB37C8664D9248", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8006), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8007), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6fd83264-c892-4f23-be65-ffb85966633a"), "912B08533926314487EFCB4855A217C6", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8030), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(8031), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("dd788770-a848-4005-b883-686bbcb622dd"), "77C7EF7542363B4D864A5E29E73CB898", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(7966), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 12, 32, 24, 947, DateTimeKind.Unspecified).AddTicks(7978), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationTokens_UserId",
                table: "VerificationTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationTokens");

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

            migrationBuilder.CreateTable(
                name: "VerificationLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
