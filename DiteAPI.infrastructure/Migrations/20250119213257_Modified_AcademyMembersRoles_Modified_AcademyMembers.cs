using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_AcademyMembersRoles_Modified_AcademyMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembers_AspNetRoles_IdentityRoleId",
                table: "AcademyMembers");

            migrationBuilder.DropIndex(
                name: "IX_AcademyMembers_IdentityRoleId",
                table: "AcademyMembers");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "AcademyMembers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "0E033819C11565489237EAF10245F2CA", new DateTimeOffset(new DateTime(2025, 1, 19, 21, 32, 57, 160, DateTimeKind.Unspecified).AddTicks(7622), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 21, 32, 57, 160, DateTimeKind.Unspecified).AddTicks(7638), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "D6676B4AC036754AAF2544D10B989228", new DateTimeOffset(new DateTime(2025, 1, 19, 21, 32, 57, 160, DateTimeKind.Unspecified).AddTicks(7658), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 21, 32, 57, 160, DateTimeKind.Unspecified).AddTicks(7659), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "4D2DCF61C9EA194DB9AC31A0F7E04742", new DateTimeOffset(new DateTime(2025, 1, 19, 21, 32, 57, 160, DateTimeKind.Unspecified).AddTicks(7678), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 19, 21, 32, 57, 160, DateTimeKind.Unspecified).AddTicks(7679), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_AcademyMembersRoles_GenericUserId",
                table: "AcademyMembersRoles",
                column: "GenericUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyMembersRoles_RoleId",
                table: "AcademyMembersRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyMembersRoles_TrackId",
                table: "AcademyMembersRoles",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_AspNetRoles_RoleId",
                table: "AcademyMembersRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_AspNetUsers_GenericUserId",
                table: "AcademyMembersRoles",
                column: "GenericUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_Tracks_TrackId",
                table: "AcademyMembersRoles",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembersRoles_AspNetRoles_RoleId",
                table: "AcademyMembersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembersRoles_AspNetUsers_GenericUserId",
                table: "AcademyMembersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembersRoles_Tracks_TrackId",
                table: "AcademyMembersRoles");

            migrationBuilder.DropIndex(
                name: "IX_AcademyMembersRoles_GenericUserId",
                table: "AcademyMembersRoles");

            migrationBuilder.DropIndex(
                name: "IX_AcademyMembersRoles_RoleId",
                table: "AcademyMembersRoles");

            migrationBuilder.DropIndex(
                name: "IX_AcademyMembersRoles_TrackId",
                table: "AcademyMembersRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityRoleId",
                table: "AcademyMembers",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
