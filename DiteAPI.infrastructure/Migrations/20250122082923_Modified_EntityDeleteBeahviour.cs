using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiteAPI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_EntityDeleteBeahviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "2DEB98074FB58147B2888A690C9E29DB", new DateTimeOffset(new DateTime(2025, 1, 22, 8, 29, 23, 52, DateTimeKind.Unspecified).AddTicks(8145), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 22, 8, 29, 23, 52, DateTimeKind.Unspecified).AddTicks(8159), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "60481A907B623E449BD03B0E7DDBF42C", new DateTimeOffset(new DateTime(2025, 1, 22, 8, 29, 23, 52, DateTimeKind.Unspecified).AddTicks(8177), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 22, 8, 29, 23, 52, DateTimeKind.Unspecified).AddTicks(8178), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "DFF2FCA5F92D3B4295174EDCBD2BF5DB", new DateTimeOffset(new DateTime(2025, 1, 22, 8, 29, 23, 52, DateTimeKind.Unspecified).AddTicks(8194), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 22, 8, 29, 23, 52, DateTimeKind.Unspecified).AddTicks(8195), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_AcademyMembersRoles_Academy_AcademyId",
                table: "AcademyMembersRoles",
                column: "AcademyId",
                principalTable: "Academy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                principalColumn: "Id");
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
    }
}
