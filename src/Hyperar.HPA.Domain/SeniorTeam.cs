namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public class SeniorTeam : HattrickEntityBase, IHattrickEntity
    {
        public byte[] AlternativeMatchKit { get; set; } = Array.Empty<byte>();

        public string AlternativeMatchKitUrl { get; set; } = string.Empty;

        public uint CoachPlayerId { get; set; }

        public DateTime FoundedOn { get; set; }

        public uint GlobalRanking { get; set; }

        public bool IsPlayingCup { get; set; }

        public bool IsPrimary { get; set; }

        public virtual League League { get; set; } = new League();

        public uint LeagueRanking { get; set; }

        public byte[]? Logo { get; set; }

        public string? LogoUrl { get; set; }

        public virtual Manager Manager { get; set; } = new Manager();

        public byte[] MatchKit { get; set; } = Array.Empty<byte>();

        public string MatchKitUrl { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<SeniorTeamOverviewMatch> OverviewMatches { get; set; } = new HashSet<SeniorTeamOverviewMatch>();

        public uint PowerRanking { get; set; }

        public virtual Region Region { get; set; } = new Region();

        public uint RegionHattrickId { get; set; }

        public uint RegionRanking { get; set; }

        public virtual ICollection<SeniorPlayer> SeniorPlayers { get; set; } = new HashSet<SeniorPlayer>();

        public uint SeniorSeriesDivision { get; set; }

        public uint SeniorSeriesHattrickId { get; set; }

        public string SeniorSeriesName { get; set; } = string.Empty;

        public virtual SeniorTeamArena? SeniorTeamArena { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public uint TeamRank { get; set; }

        public uint UndefeatedStreak { get; set; }

        public uint WinStreak { get; set; }
    }
}