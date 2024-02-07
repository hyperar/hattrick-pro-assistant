namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Common.Enums;

    public class Match
    {
        public uint? AddedMinutes { get; set; }

        public Arena Arena { get; set; } = new Arena();

        public Team AwayTeam { get; set; } = new Team();

        public List<Booking>? Bookings { get; set; }

        public uint CupLevel { get; set; }

        public uint CupLevelIndex { get; set; }

        public List<Event>? EventList { get; set; }

        public DateTime? FinishedDate { get; set; }

        public Team HomeTeam { get; set; } = new Team();

        public List<Injury>? Injuries { get; set; }

        public uint MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public uint MatchId { get; set; }

        public MatchOfficials? MatchOfficials { get; set; }

        public MatchRule MatchRuleId { get; set; }

        public MatchType MatchType { get; set; }

        public uint? PossessionFirstHalfAway { get; set; }

        public uint? PossessionFirstHalfHome { get; set; }

        public uint? PossessionSecondHalfAway { get; set; }

        public uint? PossessionSecondHalfHome { get; set; }

        public List<Goal>? Scorers { get; set; }
    }
}