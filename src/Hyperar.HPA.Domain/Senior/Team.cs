namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;

    public class Team : HattrickEntityBase, IHattrickEntity
    {
        public virtual Arena? Arena { get; set; }

        public byte[] AwayMatchKitBytes { get; set; } = Array.Empty<byte>();

        public bool CanBookMidWeekFriendly { get; set; }

        public bool CanBookWeekEndFriendly { get; set; }

        public DateTime FoundedOn { get; set; }

        public int GlobalRanking { get; set; }

        public byte[] HomeMatchKitBytes { get; set; } = Array.Empty<byte>();

        public bool IsPlayingCup { get; set; }

        public bool IsPrimary { get; set; }

        public virtual Junior.Team? JuniorTeam { get; set; }

        public virtual League League { get; set; } = new League();

        public long LeagueHattrickId { get; set; }

        public int LeagueRanking { get; set; }

        public byte[] LogoBytes { get; set; } = Array.Empty<byte>();

        public virtual Manager Manager { get; set; } = new Manager();

        public long ManagerHattrickId { get; set; }

        public virtual ICollection<Match> Matches { get; set; } = new HashSet<Match>();

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();

        public int PowerRating { get; set; }

        public virtual Region Region { get; set; } = new Region();

        public long RegionHattrickId { get; set; }

        public int RegionRanking { get; set; }

        public virtual ICollection<Series> Series { get; set; } = new HashSet<Series>();

        public string ShortName { get; set; } = string.Empty;

        public virtual ICollection<StaffMember> StaffMembers { get; set; } = new HashSet<StaffMember>();

        public int? TeamRanking { get; set; }

        public virtual Trainer? Trainer { get; set; }

        public int UndefeatedStreak { get; set; }

        public virtual ICollection<UpcomingMatch> UpcomingMatches { get; set; } = new HashSet<UpcomingMatch>();

        public int WinningStreak { get; set; }
    }
}