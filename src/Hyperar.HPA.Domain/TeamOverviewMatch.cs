namespace Hyperar.HPA.Domain
{
    using System;
    using Common.Enums;
    using Domain.Interfaces;

    public class TeamOverviewMatch : HattrickEntityBase, IHattrickEntity
    {
        public uint? AwayGoals { get; set; }

        public uint AwayTeamHattrickId { get; set; }

        public string AwayTeamName { get; set; } = string.Empty;

        public string AwayTeamShortName { get; set; } = string.Empty;

        public uint? CompetitionId { get; set; }

        public uint? HomeGoals { get; set; }

        public uint HomeTeamHattrickId { get; set; }

        public string HomeTeamName { get; set; } = string.Empty;

        public string HomeTeamShortName { get; set; } = string.Empty;

        public DateTime StartsOn { get; set; }

        public MatchStatus Status { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public MatchType Type { get; set; }
    }
}