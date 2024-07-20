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

            migrationBuilder.EnsureSchema(
                name: "junior");

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    SeasonOffset = table.Column<int>(type: "int", nullable: false),
                    SeniorNationalTeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    JuniorNationalTeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveTeams = table.Column<int>(type: "int", nullable: false),
                    ActiveUsers = table.Column<int>(type: "int", nullable: false),
                    WaitingUsers = table.Column<int>(type: "int", nullable: false),
                    LeagueLevels = table.Column<int>(type: "int", nullable: false),
                    FlagBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    SelectedTeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Country_Currency",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_League",
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
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubType = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Week = table.Column<int>(type: "int", nullable: false),
                    WeeksLeft = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "LeagueSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NextTrainingUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextEconomyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextSeriesMatchDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NextCupMatchDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FirstDailyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SecondDailyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ThirdDailyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FourthDailyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FifthDailyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueSchedule_League",
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
                    Secret = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Scope = table.Column<int>(type: "int", nullable: false),
                    GeneratedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_User",
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
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SupporterTier = table.Column<int>(type: "int", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    FoundedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsPlayingCup = table.Column<bool>(type: "bit", nullable: false),
                    GlobalRanking = table.Column<int>(type: "int", nullable: false),
                    LeagueRanking = table.Column<int>(type: "int", nullable: false),
                    RegionRanking = table.Column<int>(type: "int", nullable: false),
                    TeamRanking = table.Column<int>(type: "int", nullable: true),
                    PowerRating = table.Column<int>(type: "int", nullable: false),
                    UndefeatedStreak = table.Column<int>(type: "int", nullable: false),
                    WinningStreak = table.Column<int>(type: "int", nullable: false),
                    CanBookMidWeekFriendly = table.Column<bool>(type: "bit", nullable: false),
                    CanBookWeekEndFriendly = table.Column<bool>(type: "bit", nullable: false),
                    LogoBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HomeMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AwayMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    RegionHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Team_League",
                        column: x => x.LeagueHattrickId,
                        principalTable: "League",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Team_Manager",
                        column: x => x.ManagerHattrickId,
                        principalTable: "Manager",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Team_Region",
                        column: x => x.RegionHattrickId,
                        principalTable: "Region",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "Arena",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BuiltOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TerracesCapacity = table.Column<int>(type: "int", nullable: false),
                    BasicCapacity = table.Column<int>(type: "int", nullable: false),
                    RoofCapacity = table.Column<int>(type: "int", nullable: false),
                    VipCapacity = table.Column<int>(type: "int", nullable: false),
                    CurrentCapacity = table.Column<int>(type: "int", nullable: false),
                    ConstructionEndsOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TerracesCapacityChange = table.Column<int>(type: "int", nullable: true),
                    BasicCapacityChange = table.Column<int>(type: "int", nullable: true),
                    RoofCapacityChange = table.Column<int>(type: "int", nullable: true),
                    VipCapacityChange = table.Column<int>(type: "int", nullable: true),
                    CapacityChange = table.Column<int>(type: "int", nullable: true),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arena", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Arena_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    System = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContextId = table.Column<long>(type: "bigint", nullable: true),
                    Rules = table.Column<int>(type: "int", nullable: false),
                    AddedMinutes = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Match_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AgeYears = table.Column<int>(type: "int", nullable: false),
                    AgeDays = table.Column<int>(type: "int", nullable: false),
                    JoinedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextBirthDay = table.Column<DateTime>(type: "datetime", nullable: false),
                    ShirtNumber = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: true),
                    HasMotherClubBonus = table.Column<bool>(type: "bit", nullable: false),
                    HealthStatus = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalSkillIndex = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    IsTransferListed = table.Column<bool>(type: "bit", nullable: false),
                    Agreeability = table.Column<int>(type: "int", nullable: false),
                    Aggressiveness = table.Column<int>(type: "int", nullable: false),
                    Honesty = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<int>(type: "int", nullable: false),
                    SeniorNationalTeamCaps = table.Column<int>(type: "int", nullable: false),
                    JuniorNationalTeamCaps = table.Column<int>(type: "int", nullable: false),
                    SeasonLeagueGoals = table.Column<int>(type: "int", nullable: false),
                    SeasonCupGoals = table.Column<int>(type: "int", nullable: false),
                    SeasonFriendlyGoals = table.Column<int>(type: "int", nullable: false),
                    CareerGoals = table.Column<int>(type: "int", nullable: false),
                    CareerHattricks = table.Column<int>(type: "int", nullable: false),
                    GoalsOnTeam = table.Column<int>(type: "int", nullable: false),
                    MatchesOnTeam = table.Column<int>(type: "int", nullable: false),
                    IsForeign = table.Column<bool>(type: "bit", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Player_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Player_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                schema: "senior",
                columns: table => new
                {
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => new { x.SeriesHattrickId, x.TeamHattrickId, x.Season });
                    table.ForeignKey(
                        name: "FK_Series_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffMember",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    HiredOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_StaffMember_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                schema: "junior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FoundedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CanBookFriendlyOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrainerPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    SeniorTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Team_Team",
                        column: x => x.SeniorTeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AgeYears = table.Column<int>(type: "int", nullable: false),
                    AgeDays = table.Column<int>(type: "int", nullable: false),
                    HiredOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Trainer_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainer_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpcomingMatch",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    System = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContextId = table.Column<long>(type: "bigint", nullable: true),
                    HomeTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AwayTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpcomingMatch", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_UpcomingMatch_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchArena",
                schema: "senior",
                columns: table => new
                {
                    ArenaHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Weather = table.Column<int>(type: "int", nullable: false),
                    AttendanceTerraces = table.Column<int>(type: "int", nullable: false),
                    AttendanceBasic = table.Column<int>(type: "int", nullable: false),
                    AttendanceRoof = table.Column<int>(type: "int", nullable: false),
                    AttendanceVip = table.Column<int>(type: "int", nullable: false),
                    AttendanceTotal = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchArena", x => new { x.ArenaHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchArena_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchEvent",
                schema: "senior",
                columns: table => new
                {
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Variation = table.Column<int>(type: "int", nullable: false),
                    SubjectTeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    SubjectPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    ObjectPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    Text = table.Column<string>(type: "ntext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvent", x => new { x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchEvent_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchReferee",
                schema: "senior",
                columns: table => new
                {
                    RefereeHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchReferee", x => new { x.RefereeHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchReferee_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_MatchReferee_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                schema: "senior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Formation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TacticType = table.Column<int>(type: "int", nullable: false),
                    TacticSkill = table.Column<int>(type: "int", nullable: false),
                    FirstHalfPossession = table.Column<int>(type: "int", nullable: false),
                    SecondHalfPossession = table.Column<int>(type: "int", nullable: false),
                    MidfieldRating = table.Column<int>(type: "int", nullable: false),
                    LeftDefenseRating = table.Column<int>(type: "int", nullable: false),
                    CenterDefenseRating = table.Column<int>(type: "int", nullable: false),
                    RightDefenseRating = table.Column<int>(type: "int", nullable: false),
                    LeftAttackRating = table.Column<int>(type: "int", nullable: false),
                    CenterAttackRating = table.Column<int>(type: "int", nullable: false),
                    RightAttackRating = table.Column<int>(type: "int", nullable: false),
                    DefenseIndirectSetPiecesRating = table.Column<int>(type: "int", nullable: false),
                    AttackIndirectSetPiecesRating = table.Column<int>(type: "int", nullable: false),
                    Attitude = table.Column<int>(type: "int", nullable: true),
                    ChancesOnLeft = table.Column<int>(type: "int", nullable: false),
                    ChancesOnCenter = table.Column<int>(type: "int", nullable: false),
                    ChancesOnRight = table.Column<int>(type: "int", nullable: false),
                    SpecialEventChances = table.Column<int>(type: "int", nullable: false),
                    OtherChances = table.Column<int>(type: "int", nullable: false),
                    MatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => new { x.TeamHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchTeam_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMatch",
                schema: "senior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: false),
                    EndOfMatchRating = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatch", x => new { x.PlayerHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_PlayerMatch_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_PlayerMatch_Player",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "senior",
                        principalTable: "Player",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSkillSet",
                schema: "senior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Form = table.Column<int>(type: "int", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    Goalkeeping = table.Column<int>(type: "int", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: false),
                    Playmaking = table.Column<int>(type: "int", nullable: false),
                    Winger = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Scoring = table.Column<int>(type: "int", nullable: false),
                    SetPieces = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Loyalty = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkillSet", x => new { x.PlayerHattrickId, x.Season, x.Week });
                    table.ForeignKey(
                        name: "FK_PlayerSkillSet_Player",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "senior",
                        principalTable: "Player",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesTeam",
                schema: "senior",
                columns: table => new
                {
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Change = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    WonMatches = table.Column<int>(type: "int", nullable: false),
                    DrawnMatches = table.Column<int>(type: "int", nullable: false),
                    LostMatches = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesTeam", x => new { x.SeriesHattrickId, x.TeamHattrickId, x.Season, x.HattrickId });
                    table.ForeignKey(
                        name: "FK_SeriesTeam_Team",
                        columns: x => new { x.SeriesHattrickId, x.TeamHattrickId, x.Season },
                        principalSchema: "senior",
                        principalTable: "Series",
                        principalColumns: new[] { "SeriesHattrickId", "TeamHattrickId", "Season" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "junior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    System = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Rules = table.Column<int>(type: "int", nullable: false),
                    AddedMinutes = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Match_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "junior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                schema: "junior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AgeYears = table.Column<int>(type: "int", nullable: false),
                    AgeDays = table.Column<int>(type: "int", nullable: false),
                    JoinedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextBirthDay = table.Column<DateTime>(type: "datetime", nullable: false),
                    ShirtNumber = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: true),
                    DaysLeftToPromote = table.Column<int>(type: "int", nullable: false),
                    HealthStatus = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Specialty = table.Column<int>(type: "int", nullable: false),
                    SeasonLeagueGoals = table.Column<int>(type: "int", nullable: false),
                    SeasonFriendlyGoals = table.Column<int>(type: "int", nullable: false),
                    CareerGoals = table.Column<int>(type: "int", nullable: false),
                    CareerHattricks = table.Column<int>(type: "int", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Player_Country",
                        column: x => x.CountryHattrickId,
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Player_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "junior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                schema: "junior",
                columns: table => new
                {
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => new { x.SeriesHattrickId, x.TeamHattrickId, x.Season });
                    table.ForeignKey(
                        name: "FK_Series_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "junior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpcomingMatch",
                schema: "junior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    System = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContextId = table.Column<long>(type: "bigint", nullable: true),
                    HomeTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AwayTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpcomingMatch", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_UpcomingMatch_Team",
                        column: x => x.TeamHattrickId,
                        principalSchema: "junior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamBooking",
                schema: "senior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamBooking", x => new { x.TeamHattrickId, x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchTeamBooking_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamGoal",
                schema: "senior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamGoal", x => new { x.TeamHattrickId, x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchTeamGoal_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamInjury",
                schema: "senior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamInjury", x => new { x.TeamHattrickId, x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchTeamInjury_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUp",
                schema: "senior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    PlayStyle = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUp", x => new { x.TeamHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUp_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchArena",
                schema: "junior",
                columns: table => new
                {
                    ArenaHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Weather = table.Column<int>(type: "int", nullable: false),
                    AttendanceTerraces = table.Column<int>(type: "int", nullable: false),
                    AttendanceBasic = table.Column<int>(type: "int", nullable: false),
                    AttendanceRoof = table.Column<int>(type: "int", nullable: false),
                    AttendanceVip = table.Column<int>(type: "int", nullable: false),
                    AttendanceTotal = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchArena", x => new { x.ArenaHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchArena_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "junior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchEvent",
                schema: "junior",
                columns: table => new
                {
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Variation = table.Column<int>(type: "int", nullable: false),
                    SubjectTeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    SubjectPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    ObjectPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    Text = table.Column<string>(type: "ntext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvent", x => new { x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchEvent_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "junior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                schema: "junior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Formation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TacticType = table.Column<int>(type: "int", nullable: false),
                    TacticSkill = table.Column<int>(type: "int", nullable: false),
                    FirstHalfPossession = table.Column<int>(type: "int", nullable: false),
                    SecondHalfPossession = table.Column<int>(type: "int", nullable: false),
                    MidfieldRating = table.Column<int>(type: "int", nullable: false),
                    LeftDefenseRating = table.Column<int>(type: "int", nullable: false),
                    CenterDefenseRating = table.Column<int>(type: "int", nullable: false),
                    RightDefenseRating = table.Column<int>(type: "int", nullable: false),
                    LeftAttackRating = table.Column<int>(type: "int", nullable: false),
                    CenterAttackRating = table.Column<int>(type: "int", nullable: false),
                    RightAttackRating = table.Column<int>(type: "int", nullable: false),
                    DefenseIndirectSetPiecesRating = table.Column<int>(type: "int", nullable: false),
                    AttackIndirectSetPiecesRating = table.Column<int>(type: "int", nullable: false),
                    ChancesOnLeft = table.Column<int>(type: "int", nullable: false),
                    ChancesOnCenter = table.Column<int>(type: "int", nullable: false),
                    ChancesOnRight = table.Column<int>(type: "int", nullable: false),
                    SpecialEventChances = table.Column<int>(type: "int", nullable: false),
                    OtherChances = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => new { x.TeamHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchTeam_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "junior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMatch",
                schema: "junior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatch", x => new { x.PlayerHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_PlayerMatch_Match",
                        column: x => x.MatchHattrickId,
                        principalSchema: "junior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_PlayerMatch_Player",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "junior",
                        principalTable: "Player",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSkillSet",
                schema: "junior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Goalkeeping = table.Column<int>(type: "int", nullable: true),
                    GoalkeepingMax = table.Column<int>(type: "int", nullable: true),
                    IsGoalkeepingMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: true),
                    DefendingMax = table.Column<int>(type: "int", nullable: true),
                    IsDefendingMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    Playmaking = table.Column<int>(type: "int", nullable: true),
                    PlaymakingMax = table.Column<int>(type: "int", nullable: true),
                    IsPlaymakingMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    Winger = table.Column<int>(type: "int", nullable: true),
                    WingerMax = table.Column<int>(type: "int", nullable: true),
                    IsWingerMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: true),
                    PassingMax = table.Column<int>(type: "int", nullable: true),
                    IsPassingMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    Scoring = table.Column<int>(type: "int", nullable: true),
                    ScoringMax = table.Column<int>(type: "int", nullable: true),
                    IsScoringMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    SetPieces = table.Column<int>(type: "int", nullable: true),
                    SetPiecesMax = table.Column<int>(type: "int", nullable: true),
                    IsSetPiecesMaxReached = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkillSet", x => new { x.PlayerHattrickId, x.Season, x.Week });
                    table.ForeignKey(
                        name: "FK_PlayerSkillSet_Player",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "junior",
                        principalTable: "Player",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesTeam",
                schema: "junior",
                columns: table => new
                {
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Change = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    WonMatches = table.Column<int>(type: "int", nullable: false),
                    DrawnMatches = table.Column<int>(type: "int", nullable: false),
                    LostMatches = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesTeam", x => new { x.SeriesHattrickId, x.TeamHattrickId, x.Season, x.HattrickId });
                    table.ForeignKey(
                        name: "FK_SeriesTeam_Team",
                        columns: x => new { x.SeriesHattrickId, x.TeamHattrickId, x.Season },
                        principalSchema: "junior",
                        principalTable: "Series",
                        principalColumns: new[] { "SeriesHattrickId", "TeamHattrickId", "Season" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpPlayer",
                schema: "senior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Behavior = table.Column<int>(type: "int", nullable: true),
                    AverageRating = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: true),
                    EndOfMatchRating = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpPlayer", x => new { x.PlayerHattrickId, x.TeamHattrickId, x.MatchHattrickId, x.Role });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpPlayer_MatchTeamLineUp",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpStartingPlayer",
                schema: "senior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Behavior = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpStartingPlayer", x => new { x.PlayerHattrickId, x.TeamHattrickId, x.MatchHattrickId, x.Role });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpStartingPlayer_MatchTeamLineUp",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "senior",
                columns: table => new
                {
                    OutPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    InPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    NewRole = table.Column<int>(type: "int", nullable: false),
                    NewBehavior = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpSubstitution", x => new { x.OutPlayerHattrickId, x.InPlayerHattrickId, x.TeamHattrickId, x.MatchHattrickId, x.NewRole, x.NewBehavior });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpSubstitution_MatchTeamLineUp",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamBooking",
                schema: "junior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamBooking", x => new { x.TeamHattrickId, x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchTeamBooking_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamGoal",
                schema: "junior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamGoal", x => new { x.TeamHattrickId, x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchTeamGoal_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamInjury",
                schema: "junior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamInjury", x => new { x.TeamHattrickId, x.MatchHattrickId, x.Index });
                    table.ForeignKey(
                        name: "FK_MatchTeamInjury_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUp",
                schema: "junior",
                columns: table => new
                {
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    PlayStyle = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUp", x => new { x.TeamHattrickId, x.MatchHattrickId });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUp_MatchTeam",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeam",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpPlayer",
                schema: "junior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Behavior = table.Column<int>(type: "int", nullable: true),
                    AverageRating = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpPlayer", x => new { x.PlayerHattrickId, x.TeamHattrickId, x.MatchHattrickId, x.Role });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpPlayer_MatchTeamLineUp",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeamLineUp",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpStartingPlayer",
                schema: "junior",
                columns: table => new
                {
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Behavior = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpStartingPlayer", x => new { x.PlayerHattrickId, x.TeamHattrickId, x.MatchHattrickId, x.Role });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpStartingPlayer_MatchTeamLineUp",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeamLineUp",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "junior",
                columns: table => new
                {
                    OutPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    InPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    NewRole = table.Column<int>(type: "int", nullable: false),
                    NewBehavior = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    MatchPart = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpSubstitution", x => new { x.OutPlayerHattrickId, x.InPlayerHattrickId, x.TeamHattrickId, x.MatchHattrickId, x.NewRole, x.NewBehavior });
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpSubstitution_MatchTeamLineUp",
                        columns: x => new { x.TeamHattrickId, x.MatchHattrickId },
                        principalSchema: "junior",
                        principalTable: "MatchTeamLineUp",
                        principalColumns: new[] { "TeamHattrickId", "MatchHattrickId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arena_TeamHattrickId",
                schema: "senior",
                table: "Arena",
                column: "TeamHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_CurrencyId",
                table: "Country",
                column: "CurrencyId");

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
                name: "IX_LeagueSchedule_LeagueHattrickId",
                table: "LeagueSchedule",
                column: "LeagueHattrickId",
                unique: true);

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
                schema: "junior",
                table: "Match",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TeamHattrickId",
                schema: "senior",
                table: "Match",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchArena_MatchHattrickId",
                schema: "junior",
                table: "MatchArena",
                column: "MatchHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchArena_MatchHattrickId",
                schema: "senior",
                table: "MatchArena",
                column: "MatchHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchReferee_CountryHattrickId",
                schema: "senior",
                table: "MatchReferee",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchReferee_MatchHattrickId",
                schema: "senior",
                table: "MatchReferee",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_MatchHattrickId",
                schema: "junior",
                table: "MatchTeam",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_MatchHattrickId",
                schema: "senior",
                table: "MatchTeam",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpPlayer_TeamHattrickId_MatchHattrickId",
                schema: "junior",
                table: "MatchTeamLineUpPlayer",
                columns: new[] { "TeamHattrickId", "MatchHattrickId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpPlayer_TeamHattrickId_MatchHattrickId",
                schema: "senior",
                table: "MatchTeamLineUpPlayer",
                columns: new[] { "TeamHattrickId", "MatchHattrickId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpStartingPlayer_TeamHattrickId_MatchHattrickId",
                schema: "junior",
                table: "MatchTeamLineUpStartingPlayer",
                columns: new[] { "TeamHattrickId", "MatchHattrickId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpStartingPlayer_TeamHattrickId_MatchHattrickId",
                schema: "senior",
                table: "MatchTeamLineUpStartingPlayer",
                columns: new[] { "TeamHattrickId", "MatchHattrickId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpSubstitution_TeamHattrickId_MatchHattrickId",
                schema: "junior",
                table: "MatchTeamLineUpSubstitution",
                columns: new[] { "TeamHattrickId", "MatchHattrickId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpSubstitution_TeamHattrickId_MatchHattrickId",
                schema: "senior",
                table: "MatchTeamLineUpSubstitution",
                columns: new[] { "TeamHattrickId", "MatchHattrickId" });

            migrationBuilder.CreateIndex(
                name: "IX_Player_CountryHattrickId",
                schema: "junior",
                table: "Player",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamHattrickId",
                schema: "junior",
                table: "Player",
                column: "TeamHattrickId");

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
                name: "IX_PlayerMatch_MatchHattrickId",
                schema: "junior",
                table: "PlayerMatch",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatch_MatchHattrickId",
                schema: "senior",
                table: "PlayerMatch",
                column: "MatchHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryHattrickId",
                table: "Region",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_TeamHattrickId",
                schema: "junior",
                table: "Series",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_TeamHattrickId",
                schema: "senior",
                table: "Series",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_TeamHattrickId",
                schema: "senior",
                table: "StaffMember",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_SeniorTeamHattrickId",
                schema: "junior",
                table: "Team",
                column: "SeniorTeamHattrickId",
                unique: true);

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
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_CountryHattrickId",
                schema: "senior",
                table: "Trainer",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_TeamHattrickId",
                schema: "senior",
                table: "Trainer",
                column: "TeamHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpcomingMatch_TeamHattrickId",
                schema: "junior",
                table: "UpcomingMatch",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_UpcomingMatch_TeamHattrickId",
                schema: "senior",
                table: "UpcomingMatch",
                column: "TeamHattrickId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arena",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "LeagueCup");

            migrationBuilder.DropTable(
                name: "LeagueSchedule");

            migrationBuilder.DropTable(
                name: "MatchArena",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchArena",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchEvent",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchEvent",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchReferee",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamBooking",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamBooking",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamGoal",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamGoal",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamInjury",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamInjury",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpPlayer",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpPlayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpStartingPlayer",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpStartingPlayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "PlayerMatch",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "PlayerMatch",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "PlayerSkillSet",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "PlayerSkillSet",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "SeriesTeam",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "SeriesTeam",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "StaffMember",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Trainer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "UpcomingMatch",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "UpcomingMatch",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUp",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUp",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Series",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "Series",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeam",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "MatchTeam",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "junior");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Team",
                schema: "junior");

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
                name: "Currency");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
