using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyperar.HPA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueCup");

            migrationBuilder.DropTable(
                name: "ManagerAvatarLayer");

            migrationBuilder.DropTable(
                name: "SeniorPlayerSkill");

            migrationBuilder.DropTable(
                name: "SeniorTeamArena");

            migrationBuilder.DropTable(
                name: "SeniorTeamOverviewMatch");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "SeniorPlayer");

            migrationBuilder.DropTable(
                name: "SeniorTeam");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "League");
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
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
                    NumberOfLevels = table.Column<long>(type: "bigint", nullable: false),
                    NextTrainingUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextEconomyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextCupMatchDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextSeriesMatchDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FirstWeeklyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SecondWeeklyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ThirdWeeklyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FourthWeeklyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FifthWeeklyUpdate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.HattrickId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastDownloadDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Country_League_LeagueHattrickId",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueCup",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LeagueLevel = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    LevelIndex = table.Column<long>(type: "bigint", nullable: false),
                    CurrentRound = table.Column<long>(type: "bigint", nullable: false),
                    RoundsLeft = table.Column<long>(type: "bigint", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueCup", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_LeagueCup_League_LeagueHattrickId",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SecretValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SupporterTier = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Manager_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Manager_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerAvatarLayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    XCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    YCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ManagerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerAvatarLayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerAvatarLayer_Manager_ManagerHattrickId",
                        column: x => x.ManagerHattrickId,
                        principalTable: "Manager",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "SeniorTeam",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    FoundedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CoachPlayerId = table.Column<long>(type: "bigint", nullable: false),
                    IsPlayingCup = table.Column<bool>(type: "bit", nullable: false),
                    GlobalRanking = table.Column<long>(type: "bigint", nullable: false),
                    LeagueRanking = table.Column<long>(type: "bigint", nullable: false),
                    RegionRanking = table.Column<long>(type: "bigint", nullable: false),
                    PowerRanking = table.Column<long>(type: "bigint", nullable: false),
                    TeamRank = table.Column<long>(type: "bigint", nullable: false),
                    UndefeatedStreak = table.Column<long>(type: "bigint", nullable: false),
                    WinStreak = table.Column<long>(type: "bigint", nullable: false),
                    SeniorSeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SeniorSeriesName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeniorSeriesDivision = table.Column<long>(type: "bigint", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    MatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    AlternativeMatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MatchKit = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AlternativeMatchKit = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    RegionHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorTeam", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_SeniorTeam_League_LeagueHattrickId",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeniorTeam_Manager_ManagerHattrickId",
                        column: x => x.ManagerHattrickId,
                        principalTable: "Manager",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeniorTeam_Region_RegionHattrickId",
                        column: x => x.RegionHattrickId,
                        principalTable: "Region",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "SeniorPlayer",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShirtNumber = table.Column<long>(type: "bigint", nullable: true),
                    IsCoach = table.Column<bool>(type: "bit", nullable: false),
                    AgeYears = table.Column<long>(type: "bigint", nullable: false),
                    AgeDays = table.Column<long>(type: "bigint", nullable: false),
                    JoinedTeamOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar", nullable: true),
                    Statement = table.Column<string>(type: "nvarchar", nullable: true),
                    TotalSkillIndex = table.Column<long>(type: "bigint", nullable: false),
                    HasMotherClubBonus = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    IsForeign = table.Column<bool>(type: "bit", nullable: false),
                    Agreeability = table.Column<long>(type: "bigint", nullable: false),
                    Aggressiveness = table.Column<long>(type: "bigint", nullable: false),
                    Honesty = table.Column<long>(type: "bigint", nullable: false),
                    Leadership = table.Column<long>(type: "bigint", nullable: false),
                    Specialty = table.Column<long>(type: "bigint", nullable: false),
                    IsTransferListed = table.Column<bool>(type: "bit", nullable: false),
                    EnrolledOnNationalTeam = table.Column<bool>(type: "bit", nullable: false),
                    CurrentSeasonLeagueGoals = table.Column<long>(type: "bigint", nullable: false),
                    CurrentSeasonCupGoals = table.Column<long>(type: "bigint", nullable: false),
                    CurrentSeasonFriendlyGoals = table.Column<long>(type: "bigint", nullable: false),
                    CareerGoals = table.Column<long>(type: "bigint", nullable: false),
                    CareerHattricks = table.Column<long>(type: "bigint", nullable: false),
                    GoalsOnTeam = table.Column<long>(type: "bigint", nullable: false),
                    MatchesOnTeam = table.Column<long>(type: "bigint", nullable: false),
                    SeniorNationalTeamCaps = table.Column<long>(type: "bigint", nullable: false),
                    YouthNationalTeamCaps = table.Column<long>(type: "bigint", nullable: false),
                    BookingStatus = table.Column<long>(type: "bigint", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<long>(type: "bigint", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SeniorTeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorPlayer", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_SeniorPlayer_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeniorPlayer_SeniorTeam_SeniorTeamHattrickId",
                        column: x => x.SeniorTeamHattrickId,
                        principalTable: "SeniorTeam",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "SeniorTeamArena",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BuiltOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TerracesCapacity = table.Column<long>(type: "bigint", nullable: false),
                    BasicSeatCapacity = table.Column<long>(type: "bigint", nullable: false),
                    RoofSeatCapacity = table.Column<long>(type: "bigint", nullable: false),
                    VipLoungeCapacity = table.Column<long>(type: "bigint", nullable: false),
                    TotalCapacity = table.Column<long>(type: "bigint", nullable: false),
                    SeniorTeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorTeamArena", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_SeniorTeamArena_SeniorTeam_SeniorTeamHattrickId",
                        column: x => x.SeniorTeamHattrickId,
                        principalTable: "SeniorTeam",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeniorTeamOverviewMatch",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    HomeTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeTeamShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeGoals = table.Column<long>(type: "bigint", nullable: true),
                    AwayTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AwayTeamShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AwayGoals = table.Column<long>(type: "bigint", nullable: true),
                    StartsOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Type = table.Column<long>(type: "bigint", nullable: false),
                    CompetitionId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<long>(type: "bigint", nullable: false),
                    SeniorTeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorTeamOverviewMatch", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_SeniorTeamOverviewMatch_SeniorTeam_SeniorTeamHattrickId",
                        column: x => x.SeniorTeamHattrickId,
                        principalTable: "SeniorTeam",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "SeniorPlayerSkill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Form = table.Column<long>(type: "bigint", nullable: false),
                    Stamina = table.Column<long>(type: "bigint", nullable: false),
                    Keeper = table.Column<long>(type: "bigint", nullable: false),
                    Defending = table.Column<long>(type: "bigint", nullable: false),
                    Playmaking = table.Column<long>(type: "bigint", nullable: false),
                    Winger = table.Column<long>(type: "bigint", nullable: false),
                    Passing = table.Column<long>(type: "bigint", nullable: false),
                    Scoring = table.Column<long>(type: "bigint", nullable: false),
                    SetPieces = table.Column<long>(type: "bigint", nullable: false),
                    Experience = table.Column<long>(type: "bigint", nullable: false),
                    Loyalty = table.Column<long>(type: "bigint", nullable: false),
                    SeniorPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorPlayerSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeniorPlayerSkill_SeniorPlayer_SeniorPlayerHattrickId",
                        column: x => x.SeniorPlayerHattrickId,
                        principalTable: "SeniorPlayer",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_LeagueHattrickId",
                table: "Country",
                column: "LeagueHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueCup_LeagueHattrickId",
                table: "LeagueCup",
                column: "LeagueHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_CountryHattrickId",
                table: "Manager",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_UserId",
                table: "Manager",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagerAvatarLayer_ManagerHattrickId",
                table: "ManagerAvatarLayer",
                column: "ManagerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryHattrickId",
                table: "Region",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorPlayer_CountryHattrickId",
                table: "SeniorPlayer",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorPlayer_SeniorTeamHattrickId",
                table: "SeniorPlayer",
                column: "SeniorTeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorPlayerSkill_SeniorPlayerHattrickId",
                table: "SeniorPlayerSkill",
                column: "SeniorPlayerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeam_LeagueHattrickId",
                table: "SeniorTeam",
                column: "LeagueHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeam_ManagerHattrickId",
                table: "SeniorTeam",
                column: "ManagerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeam_RegionHattrickId",
                table: "SeniorTeam",
                column: "RegionHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeamArena_SeniorTeamHattrickId",
                table: "SeniorTeamArena",
                column: "SeniorTeamHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeniorTeamOverviewMatch_SeniorTeamHattrickId",
                table: "SeniorTeamOverviewMatch",
                column: "SeniorTeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId",
                unique: true);
        }
    }
}