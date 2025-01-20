using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_AcademyMembersRoles_GenericUser_Track_IdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "AcademyId",
                table: "AcademyMembersRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_AcademyMembersRoles_AcademyId",
                table: "AcademyMembersRoles",
                column: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_Academy_AcademyId",
                table: "AcademyMembersRoles",
                column: "AcademyId",
                principalTable: "Academy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_AspNetRoles_RoleId",
                table: "AcademyMembersRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_AspNetUsers_GenericUserId",
                table: "AcademyMembersRoles",
                column: "GenericUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_Tracks_TrackId",
                table: "AcademyMembersRoles",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademyMembersRoles_Academy_AcademyId",
                table: "AcademyMembersRoles");

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
                name: "IX_AcademyMembersRoles_AcademyId",
                table: "AcademyMembersRoles");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "AcademyMembersRoles");

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
    }
}
