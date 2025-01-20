using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_AcademyMembersRoles_Modified_AcademyMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembers_AspNetRoles_RoleId",
                table: "AcademyMembers");

            migrationBuilder.DropIndex(
                name: "IX_AcademyMembers_RoleId",
                table: "AcademyMembers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AcademyMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityRoleId",
                table: "AcademyMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcademyMembersRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenericUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyMembersRoles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "57A7ACB45C739C45BAA9322CED2F07D2", new DateTimeOffset(new DateTime(2025, 1, 19, 21, 24, 13, 661, DateTimeKind.Unspecified).AddTicks(7038), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 21, 24, 13, 661, DateTimeKind.Unspecified).AddTicks(7053), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "81C648A4AE998947A35CC0420B710562", new DateTimeOffset(new DateTime(2025, 1, 19, 21, 24, 13, 661, DateTimeKind.Unspecified).AddTicks(7084), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 21, 24, 13, 661, DateTimeKind.Unspecified).AddTicks(7085), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "A976E9D709F75D438826A66FA4F5FC1C", new DateTimeOffset(new DateTime(2025, 1, 19, 21, 24, 13, 661, DateTimeKind.Unspecified).AddTicks(7112), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 21, 24, 13, 661, DateTimeKind.Unspecified).AddTicks(7113), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_AcademyMembers_IdentityRoleId",
                table: "AcademyMembers",
                column: "IdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembers_AspNetRoles_IdentityRoleId",
                table: "AcademyMembers",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembers_AspNetRoles_IdentityRoleId",
                table: "AcademyMembers");

            migrationBuilder.DropTable(
                name: "AcademyMembersRoles");

            migrationBuilder.DropIndex(
                name: "IX_AcademyMembers_IdentityRoleId",
                table: "AcademyMembers");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "AcademyMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "AcademyMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_AcademyMembers_RoleId",
                table: "AcademyMembers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembers_AspNetRoles_RoleId",
                table: "AcademyMembers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
