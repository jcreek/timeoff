using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Jcreek.TimeOff.Data.Migrations
{
    public partial class AddBaseDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    MaximumYearlyUnusedRolloverDays = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    TeamName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    TeamMemberId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.TeamMemberId);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AnnualHolidayAllocation",
                columns: table => new
                {
                    AnnualHolidayAllocationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: true),
                    Days = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualHolidayAllocation", x => x.AnnualHolidayAllocationId);
                    table.ForeignKey(
                        name: "FK_AnnualHolidayAllocation_TeamMembers_UserId",
                        column: x => x.UserId,
                        principalTable: "TeamMembers",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.HolidayId);
                    table.ForeignKey(
                        name: "FK_Holidays_TeamMembers_UserId",
                        column: x => x.UserId,
                        principalTable: "TeamMembers",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamManagers",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    TeamMemberId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamManagers", x => new { x.TeamId, x.TeamMemberId });
                    table.ForeignKey(
                        name: "FK_TeamManagers_TeamMembers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "TeamMembers",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamManagers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "CompanyName", "MaximumYearlyUnusedRolloverDays" },
                values: new object[] { 1, "The Test Co", 5m });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CompanyId", "TeamName" },
                values: new object[,]
                {
                    { 1, 1, "Test Team 1" },
                    { 2, 1, "Test Team 2" },
                    { 3, 1, "Test Team 3" }
                });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "TeamMemberId", "Name", "TeamId" },
                values: new object[,]
                {
                    { 1, "Test Person A", 1 },
                    { 2, "Test Person B", 1 },
                    { 3, "Test Person C", 2 },
                    { 4, "Test Person D", 2 },
                    { 5, "Test Manager A", 2 },
                    { 6, "Test Manager B", 3 }
                });

            migrationBuilder.InsertData(
                table: "AnnualHolidayAllocation",
                columns: new[] { "AnnualHolidayAllocationId", "Days", "UserId", "Year" },
                values: new object[,]
                {
                    { 1, 25, 1, "2021" },
                    { 2, 25, 2, "2021" },
                    { 3, 25, 3, "2021" },
                    { 4, 25, 4, "2021" },
                    { 5, 30, 5, "2021" },
                    { 6, 30, 6, "2021" }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "HolidayId", "EndDate", "IsApproved", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2021, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "TeamManagers",
                columns: new[] { "TeamId", "TeamMemberId" },
                values: new object[,]
                {
                    { 1, 5 },
                    { 3, 5 },
                    { 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualHolidayAllocation_UserId",
                table: "AnnualHolidayAllocation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_UserId",
                table: "Holidays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamManagers_TeamMemberId",
                table: "TeamManagers",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompanyId",
                table: "Teams",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualHolidayAllocation");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "TeamManagers");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
