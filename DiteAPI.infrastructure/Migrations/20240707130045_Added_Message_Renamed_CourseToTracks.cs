using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Message_Renamed_CourseToTracks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00be60d8-2b94-4090-a582-c6e280613572"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("01919842-2a75-4788-9051-9a88ba7f6a6a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("69de252e-bcda-4e37-916a-439106707b88"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "VerificationTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "VerificationTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "OtpVerifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "OtpVerifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "AcademyMembers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AcademyMembers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeUpdated",
                table: "Academy",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Academy",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResponse = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Academy_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("1ae989e1-c1f1-43fc-8c64-1b5ed33c0cca"), "C7CCDC3E4EFB6542A250E3336F96A8B5", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2878), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2889), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f861559e-30e9-4947-8b9a-a70237677b07"), "A386B15A5CDC4D47927BBB52A42816FF", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2974), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2975), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("fb5fbbeb-77ae-4ca1-8bb5-7805ecab0de3"), "88021A5DDAC2F847B622BB378BE659DF", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2906), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 13, 0, 43, 390, DateTimeKind.Unspecified).AddTicks(2908), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AcademyId",
                table: "Tracks",
                column: "AcademyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ae989e1-c1f1-43fc-8c64-1b5ed33c0cca"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f861559e-30e9-4947-8b9a-a70237677b07"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb5fbbeb-77ae-4ca1-8bb5-7805ecab0de3"));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "VerificationTokens",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "VerificationTokens",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "OtpVerifications",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "OtpVerifications",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "AcademyMembers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "AcademyMembers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeUpdated",
                table: "Academy",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeCreated",
                table: "Academy",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Academy_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { new Guid("00be60d8-2b94-4090-a582-c6e280613572"), "C808B0E8BAE96D49ABD33F7091CAAD85", "", "Member", "MEMBER", new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1846), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1847), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("01919842-2a75-4788-9051-9a88ba7f6a6a"), "4C2E5F46B6EC154F9C89C2B772CECB61", "", "Admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1715), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1726), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("69de252e-bcda-4e37-916a-439106707b88"), "2929FE36329C9643A95C3AD792C96226", "", "Facilitator", "FACILITATOR", new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1740), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 15, 21, 29, 2, 168, DateTimeKind.Unspecified).AddTicks(1741), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Course_AcademyId",
                table: "Course",
                column: "AcademyId");
        }
    }
}
