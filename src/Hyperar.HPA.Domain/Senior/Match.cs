namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using System.Collections.Generic;
    using Common.Enums;
    using Domain.Interfaces;

    public class Match : HattrickEntityBase, IHattrickEntity
    {
        public uint? AddedMinutes { get; set; }

        public virtual MatchArena? Arena { get; set; }

        public uint AwayTeamHattrickId { get; set; }

        public uint? CompetitionId { get; set; }

        public virtual ICollection<MatchEvent> Events { get; set; } = new HashSet<MatchEvent>();

        public DateTime? FinishDate { get; set; }

        public uint HomeTeamHattrickId { get; set; }

        public virtual ICollection<MatchOfficial> Officials { get; set; } = new HashSet<MatchOfficial>();

        public MatchRule Rules { get; set; }

        public string SourceSystem { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public virtual ICollection<MatchTeam> Teams { get; set; } = new HashSet<MatchTeam>();

        public MatchType Type { get; set; }

        public Weather? Weather { get; set; }
    }
}