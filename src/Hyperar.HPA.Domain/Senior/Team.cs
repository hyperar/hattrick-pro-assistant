namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;

    public class Team : HattrickEntityBase, IHattrickEntity
    {
        public byte[] AlternativeMatchKitBytes { get; set; } = Array.Empty<byte>();

        public string AlternativeMatchKitUrl { get; set; } = string.Empty;

        public uint CoachPlayerId { get; set; }

        public DateTime FoundedOn { get; set; }

        public uint GlobalRanking { get; set; }

        public bool IsPlayingCup { get; set; }

        public bool IsPrimary { get; set; }

        public virtual League League { get; set; } = new League();

        public uint LeagueRanking { get; set; }

        public byte[]? LogoBytes { get; set; }

        public string? LogoUrl { get; set; }

        public virtual Manager Manager { get; set; } = new Manager();

        public virtual ICollection<Match> Matches { get; set; } = new HashSet<Match>();

        public byte[] MatchKitBytes { get; set; } = Array.Empty<byte>();

        public string MatchKitUrl { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<TeamOverviewMatch> OverviewMatches { get; set; } = new HashSet<TeamOverviewMatch>();

        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();

        public uint PowerRanking { get; set; }

        public virtual Region Region { get; set; } = new Region();

        public uint RegionHattrickId { get; set; }

        public uint RegionRanking { get; set; }

        public uint SeriesDivision { get; set; }

        public uint SeriesHattrickId { get; set; }

        public string SeriesName { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public virtual TeamArena? TeamArena { get; set; }

        public uint TeamRank { get; set; }

        public uint UndefeatedStreak { get; set; }

        public uint WinStreak { get; set; }
    }
}