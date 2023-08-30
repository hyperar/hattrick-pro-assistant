#nullable disable

namespace Hyperar.HPA.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Season = table.Column<long>(type: "bigint", nullable: false),
                    SeasonOffset = table.Column<int>(type: "int", nullable: false),
                    CurrentRound = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SeniorNationalTeamId = table.Column<long>(type: "bigint", nullable: false),
                    JuniorNationalTeamId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveTeams = table.Column<long>(type: "bigint", nullable: false),
                    ActiveUsers = table.Column<long>(type: "bigint", nullable: false),
                    WaitingUsers = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfLevels = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SupporterTier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TokenSecretValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TokenCreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    TokenExpiresOn = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NextTrainingUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    NextEconomyUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    NextCupMatchDate = table.Column<DateTime>(type: "date", nullable: false),
                    NextSeriesMatchDate = table.Column<DateTime>(type: "date", nullable: false),
                    FirstWeeklyUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    SecondWeeklyUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    ThirdWeeklyUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    FourthWeeklyUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    FifthWeeklyUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueCalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueCalendar_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueCup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LeagueLevel = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    LevelIndex = table.Column<long>(type: "bigint", nullable: false),
                    CurrentRound = table.Column<long>(type: "bigint", nullable: false),
                    RoundsLeft = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueCup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueCup_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_LeagueId",
                table: "Country",
                column: "LeagueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueCalendar_LeagueId",
                table: "LeagueCalendar",
                column: "LeagueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueCup_LeagueId",
                table: "LeagueCup",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId",
                table: "Region",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueCalendar");

            migrationBuilder.DropTable(
                name: "LeagueCup");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
