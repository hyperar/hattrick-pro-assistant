namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class SeniorTeam : HattrickEntityBase, IHattrickEntity
    {
        public uint CoachPlayerId { get; set; }

        public DateTime FoundedOn { get; set; }

        public uint GlobalRanking { get; set; }

        public bool IsPlayingCup { get; set; }

        public bool IsPrimary { get; set; }

        public virtual League League { get; set; } = new League();

        public uint LeagueRanking { get; set; }

        public virtual Manager Manager { get; set; } = new Manager();

        public string Name { get; set; } = string.Empty;

        public uint PowerRanking { get; set; }

        public virtual Region Region { get; set; } = new Region();

        public uint RegionHattrickId { get; set; }

        public uint RegionRanking { get; set; }

        public virtual List<SeniorPlayer>? SeniorPlayers { get; set; } = new List<SeniorPlayer>();

        public virtual SeniorTeamArena? SeniorTeamArena { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public uint TeamRank { get; set; }

        public uint UndefeatedStreak { get; set; }

        public uint WinStreak { get; set; }
    }
}