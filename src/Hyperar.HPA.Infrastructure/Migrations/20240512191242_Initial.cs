using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyperar.HPA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
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
                    Season = table.Column<byte>(type: "tinyint", nullable: false),
                    Week = table.Column<byte>(type: "tinyint", nullable: false),
                    SeasonOffset = table.Column<short>(type: "smallint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SeniorNationalTeamId = table.Column<long>(type: "bigint", nullable: false),
                    JuniorNationalTeamId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveTeams = table.Column<int>(type: "int", nullable: false),
                    ActiveUsers = table.Column<int>(type: "int", nullable: false),
                    WaitingUsers = table.Column<int>(type: "int", nullable: false),
                    NumberOfLevels = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    SubType = table.Column<byte>(type: "tinyint", nullable: true),
                    SeriesLevel = table.Column<byte>(type: "tinyint", nullable: true),
                    CurrentRound = table.Column<byte>(type: "tinyint", nullable: false),
                    RoundsLeft = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Scope = table.Column<byte>(type: "tinyint", nullable: false),
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
                    SupporterTier = table.Column<byte>(type: "tinyint", nullable: false),
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
                    GlobalRanking = table.Column<int>(type: "int", nullable: false),
                    LeagueRanking = table.Column<int>(type: "int", nullable: false),
                    RegionRanking = table.Column<int>(type: "int", nullable: false),
                    PowerRanking = table.Column<int>(type: "int", nullable: false),
                    TeamRank = table.Column<int>(type: "int", nullable: false),
                    UndefeatedStreak = table.Column<short>(type: "smallint", nullable: false),
                    WinStreak = table.Column<short>(type: "smallint", nullable: false),
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SeriesName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeriesDivision = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Age = table.Column<byte>(type: "tinyint", nullable: false),
                    JoinedTeamOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextBirthday = table.Column<DateTime>(type: "datetime", nullable: false),
                    IntroducedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpertType = table.Column<byte>(type: "tinyint", nullable: false),
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
                    System = table.Column<byte>(type: "tinyint", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    CompetitionId = table.Column<long>(type: "bigint", nullable: true),
                    Rules = table.Column<byte>(type: "tinyint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddedMinutes = table.Column<byte>(type: "tinyint", nullable: true),
                    Weather = table.Column<byte>(type: "tinyint", nullable: true),
                    Result = table.Column<byte>(type: "tinyint", nullable: true),
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
                    ShirtNumber = table.Column<byte>(type: "tinyint", nullable: true),
                    IsCoach = table.Column<bool>(type: "bit", nullable: false),
                    AgeYears = table.Column<byte>(type: "tinyint", nullable: false),
                    AgeDays = table.Column<byte>(type: "tinyint", nullable: false),
                    JoinedTeamOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Statement = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TotalSkillIndex = table.Column<int>(type: "int", nullable: false),
                    HasMotherClubBonus = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    IsForeign = table.Column<bool>(type: "bit", nullable: false),
                    Agreeability = table.Column<byte>(type: "tinyint", nullable: false),
                    Aggressiveness = table.Column<byte>(type: "tinyint", nullable: false),
                    Honesty = table.Column<byte>(type: "tinyint", nullable: false),
                    Leadership = table.Column<byte>(type: "tinyint", nullable: false),
                    Specialty = table.Column<byte>(type: "tinyint", nullable: false),
                    IsTransferListed = table.Column<bool>(type: "bit", nullable: false),
                    EnrolledOnNationalTeam = table.Column<bool>(type: "bit", nullable: false),
                    CurrentSeasonLeagueGoals = table.Column<short>(type: "smallint", nullable: false),
                    CurrentSeasonCupGoals = table.Column<short>(type: "smallint", nullable: false),
                    CurrentSeasonFriendlyGoals = table.Column<short>(type: "smallint", nullable: false),
                    CareerGoals = table.Column<short>(type: "smallint", nullable: false),
                    CareerHattricks = table.Column<short>(type: "smallint", nullable: false),
                    GoalsOnTeam = table.Column<short>(type: "smallint", nullable: false),
                    MatchesOnTeam = table.Column<short>(type: "smallint", nullable: false),
                    SeniorNationalTeamCaps = table.Column<short>(type: "smallint", nullable: false),
                    JuniorNationalTeamCaps = table.Column<short>(type: "smallint", nullable: false),
                    BookingStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Health = table.Column<short>(type: "smallint", nullable: false),
                    Category = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Level = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Index = table.Column<byte>(type: "tinyint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    AdditionalPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Variation = table.Column<short>(type: "smallint", nullable: false),
                    Text = table.Column<string>(type: "ntext", nullable: true),
                    Minute = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Location = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    MatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Formation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Score = table.Column<byte>(type: "tinyint", nullable: true),
                    TacticType = table.Column<byte>(type: "tinyint", nullable: true),
                    TacticLevel = table.Column<byte>(type: "tinyint", nullable: true),
                    MidfieldRating = table.Column<byte>(type: "tinyint", nullable: true),
                    RightDefenseRating = table.Column<byte>(type: "tinyint", nullable: true),
                    CentralDefenseRating = table.Column<byte>(type: "tinyint", nullable: true),
                    LeftDefenseRating = table.Column<byte>(type: "tinyint", nullable: true),
                    RightAttackRating = table.Column<byte>(type: "tinyint", nullable: true),
                    CentralAttackRating = table.Column<byte>(type: "tinyint", nullable: true),
                    LeftAttackRating = table.Column<byte>(type: "tinyint", nullable: true),
                    DefenseIndirectSetPiecesRating = table.Column<byte>(type: "tinyint", nullable: true),
                    AttackIndirectSetPiecesRating = table.Column<byte>(type: "tinyint", nullable: true),
                    Attitude = table.Column<short>(type: "smallint", nullable: true),
                    ChancesOnRight = table.Column<byte>(type: "tinyint", nullable: true),
                    ChancesOnCenter = table.Column<byte>(type: "tinyint", nullable: true),
                    ChancesOnLeft = table.Column<byte>(type: "tinyint", nullable: true),
                    SpecialEventChances = table.Column<byte>(type: "tinyint", nullable: true),
                    OtherChances = table.Column<byte>(type: "tinyint", nullable: true),
                    FirstHalfPosession = table.Column<byte>(type: "tinyint", nullable: true),
                    SecondHalfPosession = table.Column<byte>(type: "tinyint", nullable: true),
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
                name: "PlayerSkillSet",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season = table.Column<short>(type: "smallint", nullable: false),
                    Week = table.Column<byte>(type: "tinyint", nullable: false),
                    Form = table.Column<byte>(type: "tinyint", nullable: false),
                    Stamina = table.Column<byte>(type: "tinyint", nullable: false),
                    Keeper = table.Column<byte>(type: "tinyint", nullable: false),
                    Defending = table.Column<byte>(type: "tinyint", nullable: false),
                    Playmaking = table.Column<byte>(type: "tinyint", nullable: false),
                    Winger = table.Column<byte>(type: "tinyint", nullable: false),
                    Passing = table.Column<byte>(type: "tinyint", nullable: false),
                    Scoring = table.Column<byte>(type: "tinyint", nullable: false),
                    SetPieces = table.Column<byte>(type: "tinyint", nullable: false),
                    Loyalty = table.Column<byte>(type: "tinyint", nullable: false),
                    Experience = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Index = table.Column<byte>(type: "tinyint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Minute = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Index = table.Column<byte>(type: "tinyint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeTeamScore = table.Column<byte>(type: "tinyint", nullable: false),
                    AwayTeamScore = table.Column<byte>(type: "tinyint", nullable: false),
                    Minute = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Index = table.Column<byte>(type: "tinyint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Minute = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Experience = table.Column<byte>(type: "tinyint", nullable: false),
                    Style = table.Column<short>(type: "smallint", nullable: false),
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
                    Role = table.Column<short>(type: "smallint", nullable: false),
                    Behavior = table.Column<short>(type: "smallint", nullable: true),
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
                    Role = table.Column<short>(type: "smallint", nullable: false),
                    Behavior = table.Column<short>(type: "smallint", nullable: true),
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
                    OrderType = table.Column<byte>(type: "tinyint", nullable: false),
                    NewRole = table.Column<short>(type: "smallint", nullable: false),
                    NewRoleBehavior = table.Column<short>(type: "smallint", nullable: false),
                    Minute = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
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
    }
}
