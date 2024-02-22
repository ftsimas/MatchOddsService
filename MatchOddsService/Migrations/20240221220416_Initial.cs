using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MatchOddsService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    MatchDate = table.Column<DateTime>(type: "date", nullable: false),
                    MatchTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    TeamA = table.Column<string>(nullable: false),
                    TeamB = table.Column<string>(nullable: false),
                    Sport = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<long>(nullable: false),
                    Specifier = table.Column<int>(nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(9, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDate_TeamA_TeamB_Sport",
                table: "Matches",
                columns: new[] { "MatchDate", "TeamA", "TeamB", "Sport" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchOdds_MatchId_Specifier",
                table: "MatchOdds",
                columns: new[] { "MatchId", "Specifier" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
