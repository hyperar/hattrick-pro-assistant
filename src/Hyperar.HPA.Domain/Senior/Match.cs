namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Match : HattrickEntityBase, IHattrickEntity
    {
        public int AddedMinutes { get; set; }

        public virtual MatchArena? Arena { get; set; }

        public long? ContextId { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<MatchEvent> Events { get; set; } = new HashSet<MatchEvent>();

        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; } = new HashSet<PlayerMatch>();

        public virtual ICollection<MatchReferee> Referees { get; set; } = new HashSet<MatchReferee>();

        public MatchRule Rules { get; set; }

        public MatchSystem System { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public virtual ICollection<MatchTeam> Teams { get; set; } = new HashSet<MatchTeam>();

        public MatchType Type { get; set; }
    }
}