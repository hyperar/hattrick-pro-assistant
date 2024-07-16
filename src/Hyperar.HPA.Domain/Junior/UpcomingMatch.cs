namespace Hyperar.HPA.Domain.Junior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class UpcomingMatch : HattrickEntityBase, IHattrickEntity
    {
        public long AwayTeamHattrickId { get; set; }

        public string AwayTeamName { get; set; } = string.Empty;

        public long? ContextId { get; set; }

        public DateTime Date { get; set; }

        public long HomeTeamHattrickId { get; set; }

        public string HomeTeamName { get; set; } = string.Empty;

        public MatchSystem System { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public MatchType Type { get; set; }
    }
}