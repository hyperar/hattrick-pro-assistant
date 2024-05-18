namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using System.Linq;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick.TeamDetails;

    public class Team : HattrickEntityBase, IHattrickEntity
    {
        public Team()
        {
            this.HallOfFamePlayers = new HashSet<HallOfFamePlayer>();
            this.League = new League();
            this.Manager = new Manager();
            this.Matches = new HashSet<Match>();
            this.Players = new HashSet<Player>();
            this.Region = new Region();
            this.StaffMembers = new HashSet<StaffMember>();

            this.AwayMatchKitBytes = Array.Empty<byte>();
            this.HomeMatchKitBytes = Array.Empty<byte>();
            this.Name = string.Empty;
            this.SeriesName = string.Empty;
            this.ShortName = string.Empty;
        }

        public byte[] AwayMatchKitBytes { get; set; }

        public long CoachPlayerId { get; set; }

        public DateTime FoundedOn { get; set; }

        public int GlobalRanking { get; set; }

        public virtual ICollection<HallOfFamePlayer> HallOfFamePlayers { get; set; }

        public bool HasPromotedJuniorPlayer { get; set; }

        public byte[] HomeMatchKitBytes { get; set; } = Array.Empty<byte>();

        public bool IsPlayingCup { get; set; }

        public bool IsPrimary { get; set; }

        public virtual League League { get; set; }

        public long LeagueHattrickId { get; set; }

        public int LeagueRanking { get; set; }

        public byte[]? LogoBytes { get; set; }

        public virtual Manager Manager { get; set; }

        public long ManagerHattrickId { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public int PowerRanking { get; set; }

        public virtual Region Region { get; set; }

        public long RegionHattrickId { get; set; }

        public int RegionRanking { get; set; }

        public int SeriesDivision { get; set; }

        public long SeriesHattrickId { get; set; }

        public string SeriesName { get; set; }

        public string ShortName { get; set; }

        public virtual ICollection<StaffMember> StaffMembers { get; set; }

        public virtual TeamArena? TeamArena { get; set; }

        public int TeamRank { get; set; }

        public long TrainerHattrickId { get; set; }

        public int UndefeatedStreak { get; set; }

        public int WinStreak { get; set; }

        public static Team Create(
            Models.Team xmlTeam,
            byte[]? logoBytes,
            byte[] homeMatchKitBytes,
            byte[] awayMatchKitBytes,
            League league,
            Manager manager,
            Region region)
        {
            return new Team
            {
                AwayMatchKitBytes = awayMatchKitBytes,
                CoachPlayerId = xmlTeam.Trainer.PlayerId,
                FoundedOn = xmlTeam.FoundedDate,
                GlobalRanking = xmlTeam.PowerRating.GlobalRanking,
                HattrickId = xmlTeam.TeamId,
                HomeMatchKitBytes = homeMatchKitBytes,
                IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup,
                IsPrimary = xmlTeam.IsPrimaryClub,
                League = league,
                LeagueRanking = xmlTeam.PowerRating.LeagueRanking,
                LogoBytes = logoBytes,
                Manager = manager,
                Name = xmlTeam.TeamName,
                PowerRanking = xmlTeam.PowerRating.PowerRating,
                Region = region,
                RegionRanking = xmlTeam.PowerRating.RegionRanking,
                SeriesDivision = xmlTeam.LeagueLevelUnit.LeagueLevel,
                SeriesName = xmlTeam.LeagueLevelUnit.LeagueLevelUnitName,
                SeriesHattrickId = xmlTeam.LeagueLevelUnit.LeagueLevelUnitId,
                ShortName = xmlTeam.ShortTeamName,
                TeamRank = xmlTeam.TeamRank ?? 0,
                TrainerHattrickId = xmlTeam.Trainer.PlayerId,
                UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0,
                WinStreak = xmlTeam.NumberOfVictories ?? 0
            };
        }

        public bool HasChanged(
            Models.Team xmlTeam,
            byte[]? logoBytes,
            byte[] homeMatchKitBytes,
            byte[] awayMatchKitBytes)
        {
            return !this.AwayMatchKitBytes.SequenceEqual(awayMatchKitBytes)
                || this.CoachPlayerId != xmlTeam.Trainer.PlayerId
                || this.GlobalRanking != xmlTeam.PowerRating.GlobalRanking
                || !this.HomeMatchKitBytes.SequenceEqual(homeMatchKitBytes)
                || this.IsPlayingCup != (xmlTeam.Cup != null && xmlTeam.Cup.StillInCup)
                || this.LeagueRanking != xmlTeam.PowerRating.LeagueRanking
                || !(this.LogoBytes ?? Array.Empty<byte>()).SequenceEqual(logoBytes ?? Array.Empty<byte>())
                || this.Name != xmlTeam.TeamName
                || this.PowerRanking != xmlTeam.PowerRating.PowerRating
                || this.RegionHattrickId != xmlTeam.Region.Id
                || this.RegionRanking != xmlTeam.PowerRating.RegionRanking
                || this.SeriesDivision != xmlTeam.LeagueLevelUnit.LeagueLevel
                || this.SeriesHattrickId != xmlTeam.LeagueLevelUnit.LeagueLevelUnitId
                || this.SeriesName != xmlTeam.LeagueLevelUnit.LeagueLevelUnitName
                || this.ShortName != xmlTeam.ShortTeamName
                || this.TeamRank != xmlTeam.TeamRank
                || this.TrainerHattrickId != xmlTeam.Trainer.PlayerId
                || this.UndefeatedStreak != xmlTeam.NumberOfUndefeated
                || this.WinStreak != xmlTeam.NumberOfVictories;
        }

        public void Update(
            Models.Team xmlTeam,
            byte[]? logoBytes,
            byte[] homeMatchKitBytes,
            byte[] awayMatchKitBytes)
        {
            this.AwayMatchKitBytes = awayMatchKitBytes;
            this.CoachPlayerId = xmlTeam.Trainer.PlayerId;
            this.GlobalRanking = xmlTeam.PowerRating.GlobalRanking;
            this.HomeMatchKitBytes = homeMatchKitBytes;
            this.IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup;
            this.LeagueRanking = xmlTeam.PowerRating.LeagueRanking;
            this.LogoBytes = logoBytes;
            this.Name = xmlTeam.TeamName;
            this.PowerRanking = xmlTeam.PowerRating.PowerRating;
            this.RegionHattrickId = xmlTeam.Region.Id;
            this.RegionRanking = xmlTeam.PowerRating.RegionRanking;
            this.SeriesDivision = xmlTeam.LeagueLevelUnit.LeagueLevel;
            this.SeriesHattrickId = xmlTeam.LeagueLevelUnit.LeagueLevelUnitId;
            this.SeriesName = xmlTeam.LeagueLevelUnit.LeagueLevelUnitName;
            this.ShortName = xmlTeam.ShortTeamName;
            this.TeamRank = xmlTeam.TeamRank ?? 0;
            this.TrainerHattrickId = xmlTeam.Trainer.PlayerId;
            this.UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0;
            this.WinStreak = xmlTeam.NumberOfVictories ?? 0;
        }
    }
}