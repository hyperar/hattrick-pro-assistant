﻿using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "MatchArena",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchEvent",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchOfficial",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamBooking",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamGoal",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamInjury",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpPlayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpStartingPlayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "PlayerMatch",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "PlayerSkillSet",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "StaffMember",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "TeamArena",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUp",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "HallOfFamePlayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeam",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Team",
                schema: "senior");

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
            migrationBuilder.EnsureSchema(
                name: "senior");

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
                    Season = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    SeasonOffset = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SeniorNationalTeamId = table.Column<long>(type: "bigint", nullable: false),
                    JuniorNationalTeamId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveTeams = table.Column<int>(type: "int", nullable: false),
                    ActiveUsers = table.Column<int>(type: "int", nullable: false),
                    WaitingUsers = table.Column<int>(type: "int", nullable: false),
                    NumberOfLevels = table.Column<int>(type: "int", nullable: false),
                    NextTrainingUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextEconomyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextCupMatchDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NextSeriesMatchDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FlagBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
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
                    LastDownloadDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastSelectedTeamHattrickId = table.Column<long>(type: "bigint", nullable: true)
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
                        name: "FK_Country_League",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "LeagueCup",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubType = table.Column<int>(type: "int", nullable: true),
                    SeriesLevel = table.Column<int>(type: "int", nullable: true),
                    CurrentRound = table.Column<int>(type: "int", nullable: false),
                    RoundsLeft = table.Column<int>(type: "int", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueCup", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_LeagueCup_League",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Token_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SupporterTier = table.Column<int>(type: "int", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Manager_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Manager_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
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
                        name: "FK_Region_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "Team",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    FoundedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CoachPlayerId = table.Column<long>(type: "bigint", nullable: false),
                    IsPlayingCup = table.Column<bool>(type: "bit", nullable: false),
                    HasPromotedJuniorPlayer = table.Column<bool>(type: "bit", nullable: false),
                    GlobalRanking = table.Column<int>(type: "int", nullable: false),
                    LeagueRanking = table.Column<int>(type: "int", nullable: false),
                    RegionRanking = table.Column<int>(type: "int", nullable: false),
                    PowerRanking = table.Column<int>(type: "int", nullable: false),
                    TeamRank = table.Column<int>(type: "int", nullable: false),
                    UndefeatedStreak = table.Column<int>(type: "int", nullable: false),
                    WinStreak = table.Column<int>(type: "int", nullable: false),
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SeriesName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeriesDivision = table.Column<int>(type: "int", nullable: false),
                    TrainerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    LogoBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HomeMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AwayMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    RegionHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Senior_Team_League",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Senior_Team_Manager",
                        column: x => x.ManagerHattrickId,
                        principalTable: "Manager",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Senior_Team_Region",
                        column: x => x.RegionHattrickId,
                        principalTable: "Region",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "HallOfFamePlayer",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    JoinedTeamOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextBirthday = table.Column<DateTime>(type: "datetime", nullable: false),
                    IntroducedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpertType = table.Column<int>(type: "int", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallOfFamePlayer", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Senior_HallOfFamePlayer_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Senior_HallOfFamePlayer_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    System = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<long>(type: "bigint", nullable: true),
                    Rules = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddedMinutes = table.Column<int>(type: "int", nullable: true),
                    Weather = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: true),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Senior_Match_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShirtNumber = table.Column<int>(type: "int", nullable: true),
                    IsCoach = table.Column<bool>(type: "bit", nullable: false),
                    AgeYears = table.Column<int>(type: "int", nullable: false),
                    AgeDays = table.Column<int>(type: "int", nullable: false),
                    NextBirthDay = table.Column<DateTime>(type: "datetime", nullable: false),
                    JoinedTeamOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Statement = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TotalSkillIndex = table.Column<int>(type: "int", nullable: false),
                    HasMotherClubBonus = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    IsForeign = table.Column<bool>(type: "bit", nullable: false),
                    Agreeability = table.Column<int>(type: "int", nullable: false),
                    Aggressiveness = table.Column<int>(type: "int", nullable: false),
                    Honesty = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<int>(type: "int", nullable: false),
                    IsTransferListed = table.Column<bool>(type: "bit", nullable: false),
                    CurrentSeasonLeagueGoals = table.Column<int>(type: "int", nullable: false),
                    CurrentSeasonCupGoals = table.Column<int>(type: "int", nullable: false),
                    CurrentSeasonFriendlyGoals = table.Column<int>(type: "int", nullable: false),
                    CareerGoals = table.Column<int>(type: "int", nullable: false),
                    CareerHattricks = table.Column<int>(type: "int", nullable: false),
                    GoalsOnTeam = table.Column<int>(type: "int", nullable: false),
                    MatchesOnTeam = table.Column<int>(type: "int", nullable: false),
                    SeniorNationalTeamCaps = table.Column<int>(type: "int", nullable: false),
                    JuniorNationalTeamCaps = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    AskingPrice = table.Column<long>(type: "bigint", nullable: true),
                    BuyingTeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    BuyingTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    WinningBid = table.Column<long>(type: "bigint", nullable: true),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Senior_Player_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Senior_Player_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "TeamArena",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BuiltOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TerracesCapacity = table.Column<int>(type: "int", nullable: false),
                    BasicSeatCapacity = table.Column<int>(type: "int", nullable: false),
                    RoofSeatCapacity = table.Column<int>(type: "int", nullable: false),
                    VipLoungeCapacity = table.Column<int>(type: "int", nullable: false),
                    TotalCapacity = table.Column<int>(type: "int", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamArena", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Senior_TeamArena_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "StaffMember",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HiredOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HallOfFamePlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_StaffMember_HallOfFamePlayer",
                        column: x => x.HallOfFamePlayerHattrickId,
                        principalSchema: "senior",
                        principalTable: "HallOfFamePlayer",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_StaffMember_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "MatchArena",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Attendance = table.Column<int>(type: "int", nullable: true),
                    TerracesSold = table.Column<int>(type: "int", nullable: true),
                    BasicSeatsSold = table.Column<int>(type: "int", nullable: true),
                    RoofSeatsSold = table.Column<int>(type: "int", nullable: true),
                    VipSeatsSold = table.Column<int>(type: "int", nullable: true),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchArena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchArena_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "MatchEvent",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    AdditionalPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Variation = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "ntext", nullable: true),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchEvent_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "MatchOfficial",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOfficial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchOfficial_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Senior_MatchOfficial_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    MatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    MatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Formation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TacticType = table.Column<int>(type: "int", nullable: true),
                    TacticLevel = table.Column<int>(type: "int", nullable: true),
                    MidfieldRating = table.Column<int>(type: "int", nullable: true),
                    RightDefenseRating = table.Column<int>(type: "int", nullable: true),
                    CentralDefenseRating = table.Column<int>(type: "int", nullable: true),
                    LeftDefenseRating = table.Column<int>(type: "int", nullable: true),
                    RightAttackRating = table.Column<int>(type: "int", nullable: true),
                    CentralAttackRating = table.Column<int>(type: "int", nullable: true),
                    LeftAttackRating = table.Column<int>(type: "int", nullable: true),
                    DefenseIndirectSetPiecesRating = table.Column<int>(type: "int", nullable: true),
                    AttackIndirectSetPiecesRating = table.Column<int>(type: "int", nullable: true),
                    Attitude = table.Column<int>(type: "int", nullable: true),
                    ChancesOnRight = table.Column<int>(type: "int", nullable: true),
                    ChancesOnCenter = table.Column<int>(type: "int", nullable: true),
                    ChancesOnLeft = table.Column<int>(type: "int", nullable: true),
                    SpecialEventChances = table.Column<int>(type: "int", nullable: true),
                    OtherChances = table.Column<int>(type: "int", nullable: true),
                    FirstHalfPosession = table.Column<int>(type: "int", nullable: true),
                    SecondHalfPosession = table.Column<int>(type: "int", nullable: true),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeam_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "PlayerMatch",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    EndOfMatchRating = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    CalculatedRating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMatch_Player",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "senior",
                        principalTable: "Player",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "PlayerSkillSet",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Form = table.Column<int>(type: "int", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    Keeper = table.Column<int>(type: "int", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: false),
                    Playmaking = table.Column<int>(type: "int", nullable: false),
                    Winger = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Scoring = table.Column<int>(type: "int", nullable: false),
                    SetPieces = table.Column<int>(type: "int", nullable: false),
                    Loyalty = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkillSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerSkillSet_Player",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "senior",
                        principalTable: "Player",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamBooking",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    MatchTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamBooking_MatchTeam",
                        column: x => x.MatchTeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamGoal",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    MatchTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamGoal_MatchTeam",
                        column: x => x.MatchTeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamInjury",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    MatchTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamInjury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamInjury_MatchTeam",
                        column: x => x.MatchTeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUp",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<int>(type: "int", nullable: false),
                    MatchTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamLineUp_MatchTeam",
                        column: x => x.MatchTeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpPlayer",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Behavior = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    EndRating = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    MatchTeamLineUpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamLineUpPlayer_MatchTeamLineUp",
                        column: x => x.MatchTeamLineUpId,
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpStartingPlayer",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Behavior = table.Column<int>(type: "int", nullable: true),
                    MatchTeamLineUpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpStartingPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamLineUpStartingPlayer_MatchTeamLineUp",
                        column: x => x.MatchTeamLineUpId,
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    NewRole = table.Column<int>(type: "int", nullable: false),
                    NewRoleBehavior = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    InPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    OutPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchTeamLineUpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpSubstitution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senior_MatchTeamLineUpSubstitution_MatchTeamLineUp",
                        column: x => x.MatchTeamLineUpId,
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_LeagueHattrickId",
                table: "Country",
                column: "LeagueHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HallOfFamePlayer_CountryHattrickId",
                schema: "senior",
                table: "HallOfFamePlayer",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_HallOfFamePlayer_TeamHattrickId",
                schema: "senior",
                table: "HallOfFamePlayer",
                column: "TeamHattrickId");

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
                name: "IX_Match_TeamHattrickId",
                schema: "senior",
                table: "Match",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchArena_MatchHattrickId",
                schema: "senior",
                table: "MatchArena",
                column: "MatchHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvent_MatchHattrickId",
                schema: "senior",
                table: "MatchEvent",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchOfficial_CountryHattrickId",
                schema: "senior",
                table: "MatchOfficial",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchOfficial_MatchHattrickId",
                schema: "senior",
                table: "MatchOfficial",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_MatchHattrickId",
                schema: "senior",
                table: "MatchTeam",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamBooking_MatchTeamId",
                schema: "senior",
                table: "MatchTeamBooking",
                column: "MatchTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamGoal_MatchTeamId",
                schema: "senior",
                table: "MatchTeamGoal",
                column: "MatchTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamInjury_MatchTeamId",
                schema: "senior",
                table: "MatchTeamInjury",
                column: "MatchTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUp_MatchTeamId",
                schema: "senior",
                table: "MatchTeamLineUp",
                column: "MatchTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpPlayer_MatchTeamLineUpId",
                schema: "senior",
                table: "MatchTeamLineUpPlayer",
                column: "MatchTeamLineUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpStartingPlayer_MatchTeamLineUpId",
                schema: "senior",
                table: "MatchTeamLineUpStartingPlayer",
                column: "MatchTeamLineUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpSubstitution_MatchTeamLineUpId",
                schema: "senior",
                table: "MatchTeamLineUpSubstitution",
                column: "MatchTeamLineUpId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_CountryHattrickId",
                schema: "senior",
                table: "Player",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamHattrickId",
                schema: "senior",
                table: "Player",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatch_PlayerHattrickId",
                schema: "senior",
                table: "PlayerMatch",
                column: "PlayerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkillSet_PlayerHattrickId",
                schema: "senior",
                table: "PlayerSkillSet",
                column: "PlayerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryHattrickId",
                table: "Region",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_HallOfFamePlayerHattrickId",
                schema: "senior",
                table: "StaffMember",
                column: "HallOfFamePlayerHattrickId",
                unique: true,
                filter: "[HallOfFamePlayerHattrickId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_TeamHattrickId",
                schema: "senior",
                table: "StaffMember",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LeagueHattrickId",
                schema: "senior",
                table: "Team",
                column: "LeagueHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ManagerHattrickId",
                schema: "senior",
                table: "Team",
                column: "ManagerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_RegionHattrickId",
                schema: "senior",
                table: "Team",
                column: "RegionHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamArena_TeamHattrickId",
                schema: "senior",
                table: "TeamArena",
                column: "TeamHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId",
                unique: true);
        }
    }
}