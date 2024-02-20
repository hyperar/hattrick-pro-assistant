#nullable disable

namespace Hyperar.HPA.Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueCup",
                schema: "global");

            migrationBuilder.DropTable(
                name: "ManagerAvatarLayer",
                schema: "global");

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
                name: "PlayerAvatarLayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "PlayerSkillSet",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "StaffMemberAvatarLayer",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "TeamArena",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "TeamOverviewMatch",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUpSubstitution",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "StaffMember",
                schema: "senior");

            migrationBuilder.DropTable(
                name: "MatchTeamLineUp",
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
                name: "Manager",
                schema: "global");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "global");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "global");

            migrationBuilder.DropTable(
                name: "League",
                schema: "global");
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "global");

            migrationBuilder.EnsureSchema(
                name: "senior");

            migrationBuilder.CreateTable(
                name: "League",
                schema: "global",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Season = table.Column<long>(type: "bigint", nullable: false),
                    Week = table.Column<long>(type: "bigint", nullable: false),
                    SeasonOffset = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NationalTeamId = table.Column<long>(type: "bigint", nullable: false),
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
                    FifthWeeklyUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
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
                    LastDownloadDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "global",
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
                        principalSchema: "global",
                        principalTable: "League",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueCup",
                schema: "global",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LeagueLevel = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    LevelIndex = table.Column<long>(type: "bigint", nullable: false),
                    CurrentRound = table.Column<int>(type: "int", nullable: false),
                    RoundsLeft = table.Column<long>(type: "bigint", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueCup", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_LeagueCup_League_LeagueHattrickId",
                        column: x => x.LeagueHattrickId,
                        principalSchema: "global",
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
                name: "HallOfFamePlayer",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Age = table.Column<long>(type: "bigint", nullable: false),
                    JoinedTeamOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextBirthday = table.Column<DateTime>(type: "datetime", nullable: false),
                    IntroducedToHallOfFameOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpertType = table.Column<byte>(type: "tinyint", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallOfFamePlayer", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_HallOfFamePlayer_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalSchema: "global",
                        principalTable: "Country",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                schema: "global",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SupporterTier = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_Manager_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalSchema: "global",
                        principalTable: "Country",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Manager_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "global",
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
                        principalSchema: "global",
                        principalTable: "Country",
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
                    HiredOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HallOfFamePlayerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_StaffMember_HallOfFamePlayer_HallOfFamePlayerId",
                        column: x => x.HallOfFamePlayerId,
                        principalSchema: "senior",
                        principalTable: "HallOfFamePlayer",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "ManagerAvatarLayer",
                schema: "global",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    XCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    YCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ManagerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerAvatarLayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerAvatarLayer_Manager_ManagerHattrickId",
                        column: x => x.ManagerHattrickId,
                        principalSchema: "global",
                        principalTable: "Manager",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
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
                    GlobalRanking = table.Column<long>(type: "bigint", nullable: false),
                    LeagueRanking = table.Column<long>(type: "bigint", nullable: false),
                    RegionRanking = table.Column<long>(type: "bigint", nullable: false),
                    PowerRanking = table.Column<long>(type: "bigint", nullable: false),
                    TeamRank = table.Column<long>(type: "bigint", nullable: false),
                    UndefeatedStreak = table.Column<long>(type: "bigint", nullable: false),
                    WinStreak = table.Column<long>(type: "bigint", nullable: false),
                    SeriesHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SeriesName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeriesDivision = table.Column<long>(type: "bigint", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    MatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    AlternativeMatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    LogoBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AlternativeMatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LeagueHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    RegionHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Team_League_LeagueHattrickId",
                        column: x => x.LeagueHattrickId,
                        principalSchema: "global",
                        principalTable: "League",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Team_Manager_ManagerHattrickId",
                        column: x => x.ManagerHattrickId,
                        principalSchema: "global",
                        principalTable: "Manager",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Team_Region_RegionHattrickId",
                        column: x => x.RegionHattrickId,
                        principalSchema: "global",
                        principalTable: "Region",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffMemberAvatarLayer",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    XCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    YCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    StaffHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMemberAvatarLayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffMemberAvatarLayer_StaffMember_StaffHattrickId",
                        column: x => x.StaffHattrickId,
                        principalSchema: "senior",
                        principalTable: "StaffMember",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "senior",
                columns: table => new
                {
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    SourceSystem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    CompetitionId = table.Column<long>(type: "bigint", nullable: true),
                    Rules = table.Column<byte>(type: "tinyint", nullable: false),
                    HomeTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    AwayTeamHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddedMinutes = table.Column<long>(type: "bigint", nullable: true),
                    Weather = table.Column<byte>(type: "tinyint", nullable: true),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Match_Team_TeamHattrickId",
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
                    Agreeability = table.Column<byte>(type: "tinyint", nullable: false),
                    Aggressiveness = table.Column<byte>(type: "tinyint", nullable: false),
                    Honesty = table.Column<byte>(type: "tinyint", nullable: false),
                    Leadership = table.Column<byte>(type: "tinyint", nullable: false),
                    Specialty = table.Column<byte>(type: "tinyint", nullable: false),
                    IsTransferListed = table.Column<bool>(type: "bit", nullable: false),
                    EnrolledOnNationalTeam = table.Column<bool>(type: "bit", nullable: false),
                    CurrentSeasonLeagueGoals = table.Column<long>(type: "bigint", nullable: false),
                    CurrentSeasonCupGoals = table.Column<long>(type: "bigint", nullable: false),
                    CurrentSeasonFriendlyGoals = table.Column<long>(type: "bigint", nullable: false),
                    CareerGoals = table.Column<long>(type: "bigint", nullable: false),
                    CareerHattricks = table.Column<long>(type: "bigint", nullable: false),
                    GoalsOnTeam = table.Column<long>(type: "bigint", nullable: false),
                    MatchesOnTeam = table.Column<long>(type: "bigint", nullable: false),
                    NationalTeamCaps = table.Column<long>(type: "bigint", nullable: false),
                    YouthNationalTeamCaps = table.Column<long>(type: "bigint", nullable: false),
                    BookingStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<byte>(type: "tinyint", nullable: false),
                    AvatarBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CountryHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_Player_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalSchema: "global",
                        principalTable: "Country",
                        principalColumn: "HattrickId");
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamHattrickId",
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
                    TerracesCapacity = table.Column<long>(type: "bigint", nullable: false),
                    BasicSeatCapacity = table.Column<long>(type: "bigint", nullable: false),
                    RoofSeatCapacity = table.Column<long>(type: "bigint", nullable: false),
                    VipLoungeCapacity = table.Column<long>(type: "bigint", nullable: false),
                    TotalCapacity = table.Column<long>(type: "bigint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamArena", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_TeamArena_Team_TeamHattrickId",
                        column: x => x.TeamHattrickId,
                        principalSchema: "senior",
                        principalTable: "Team",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamOverviewMatch",
                schema: "senior",
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
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    CompetitionId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamOverviewMatch", x => x.HattrickId);
                    table.ForeignKey(
                        name: "FK_TeamOverviewMatch_Team_TeamHattrickId",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HattrickId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Attendance = table.Column<long>(type: "bigint", nullable: true),
                    TerracesSold = table.Column<long>(type: "bigint", nullable: true),
                    BasicSeatsSold = table.Column<long>(type: "bigint", nullable: true),
                    RoofSeatsSold = table.Column<long>(type: "bigint", nullable: true),
                    VipSeatsSold = table.Column<long>(type: "bigint", nullable: true),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchArena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchArena_Match_MatchHattrickId",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    AdditionalPlayerHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    TeamHattrickId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<long>(type: "bigint", nullable: false),
                    Variation = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "ntext", maxLength: 8000, nullable: true),
                    Minute = table.Column<long>(type: "bigint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchEvent_Match_MatchHattrickId",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_MatchOfficial_Country_CountryHattrickId",
                        column: x => x.CountryHattrickId,
                        principalSchema: "global",
                        principalTable: "Country",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchOfficial_Match_MatchHattrickId",
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
                    MatchKitUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    MatchKitBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Formation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Score = table.Column<long>(type: "bigint", nullable: true),
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
                    ChancesOnRight = table.Column<long>(type: "bigint", nullable: true),
                    ChancesOnCenter = table.Column<long>(type: "bigint", nullable: true),
                    ChancesOnLeft = table.Column<long>(type: "bigint", nullable: true),
                    SpecialEventChances = table.Column<long>(type: "bigint", nullable: true),
                    OtherChances = table.Column<long>(type: "bigint", nullable: true),
                    FirstHalfPosession = table.Column<long>(type: "bigint", nullable: true),
                    SecondHalfPosession = table.Column<long>(type: "bigint", nullable: true),
                    MatchHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeam_Match_MatchHattrickId",
                        column: x => x.MatchHattrickId,
                        principalSchema: "senior",
                        principalTable: "Match",
                        principalColumn: "HattrickId");
                });

            migrationBuilder.CreateTable(
                name: "PlayerAvatarLayer",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    XCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    YCoordinate = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAvatarLayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAvatarLayer_Player_PlayerHattrickId",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season = table.Column<long>(type: "bigint", nullable: false),
                    Week = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_PlayerSkillSet_Player_PlayerHattrickId",
                        column: x => x.PlayerHattrickId,
                        principalSchema: "senior",
                        principalTable: "Player",
                        principalColumn: "HattrickId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamBooking",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Minute = table.Column<long>(type: "bigint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamBooking_MatchTeam_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamGoal",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HomeTeamScore = table.Column<long>(type: "bigint", nullable: false),
                    AwayTeamScore = table.Column<long>(type: "bigint", nullable: false),
                    Minute = table.Column<long>(type: "bigint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamGoal_MatchTeam_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamInjury",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    PlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Minute = table.Column<long>(type: "bigint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamInjury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamInjury_MatchTeam_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamLineUp",
                schema: "senior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Experience = table.Column<byte>(type: "tinyint", nullable: false),
                    Style = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUp_MatchTeam_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "senior",
                        principalTable: "MatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Rating = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    EndRating = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    LineUpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpPlayer_MatchTeamLineUp_LineUpId",
                        column: x => x.LineUpId,
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
                    Minute = table.Column<long>(type: "bigint", nullable: false),
                    MatchPart = table.Column<byte>(type: "tinyint", nullable: false),
                    InPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false),
                    LineUpId = table.Column<int>(type: "int", nullable: false),
                    OutPlayerHattrickId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpSubstitution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpSubstitution_MatchTeamLineUp_LineUpId",
                        column: x => x.LineUpId,
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
                    LineUpId = table.Column<int>(type: "int", nullable: false),
                    SubstitutionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamLineUpStartingPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpStartingPlayer_MatchTeamLineUpSubstitution_SubstitutionId",
                        column: x => x.SubstitutionId,
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUpSubstitution",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchTeamLineUpStartingPlayer_MatchTeamLineUp_LineUpId",
                        column: x => x.LineUpId,
                        principalSchema: "senior",
                        principalTable: "MatchTeamLineUp",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_LeagueHattrickId",
                schema: "global",
                table: "Country",
                column: "LeagueHattrickId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HallOfFamePlayer_CountryHattrickId",
                schema: "senior",
                table: "HallOfFamePlayer",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueCup_LeagueHattrickId",
                schema: "global",
                table: "LeagueCup",
                column: "LeagueHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_CountryHattrickId",
                schema: "global",
                table: "Manager",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_UserId",
                schema: "global",
                table: "Manager",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagerAvatarLayer_ManagerHattrickId",
                schema: "global",
                table: "ManagerAvatarLayer",
                column: "ManagerHattrickId");

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
                name: "IX_MatchTeamBooking_TeamId",
                schema: "senior",
                table: "MatchTeamBooking",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamGoal_TeamId",
                schema: "senior",
                table: "MatchTeamGoal",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamInjury_TeamId",
                schema: "senior",
                table: "MatchTeamInjury",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUp_TeamId",
                schema: "senior",
                table: "MatchTeamLineUp",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpPlayer_LineUpId",
                schema: "senior",
                table: "MatchTeamLineUpPlayer",
                column: "LineUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpStartingPlayer_LineUpId",
                schema: "senior",
                table: "MatchTeamLineUpStartingPlayer",
                column: "LineUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpStartingPlayer_SubstitutionId",
                schema: "senior",
                table: "MatchTeamLineUpStartingPlayer",
                column: "SubstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamLineUpSubstitution_LineUpId",
                schema: "senior",
                table: "MatchTeamLineUpSubstitution",
                column: "LineUpId");

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
                name: "IX_PlayerAvatarLayer_PlayerHattrickId",
                schema: "senior",
                table: "PlayerAvatarLayer",
                column: "PlayerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkillSet_PlayerHattrickId",
                schema: "senior",
                table: "PlayerSkillSet",
                column: "PlayerHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryHattrickId",
                schema: "global",
                table: "Region",
                column: "CountryHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_HallOfFamePlayerId",
                schema: "senior",
                table: "StaffMember",
                column: "HallOfFamePlayerId",
                unique: true,
                filter: "[HallOfFamePlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMemberAvatarLayer_StaffHattrickId",
                schema: "senior",
                table: "StaffMemberAvatarLayer",
                column: "StaffHattrickId");

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
                name: "IX_TeamOverviewMatch_TeamHattrickId",
                schema: "senior",
                table: "TeamOverviewMatch",
                column: "TeamHattrickId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId",
                unique: true);
        }
    }
}