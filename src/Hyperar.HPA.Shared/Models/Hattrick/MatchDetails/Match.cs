namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    using System;
    using System.Collections.Generic;

    public class Match
    {
        public Match()
        {
            this.Arena = new Arena();
            this.AwayTeam = new Team();
            this.HomeTeam = new Team();
        }

        public int? AddedMinutes { get; set; }

        public Arena Arena { get; set; }

        public Team AwayTeam { get; set; }

        public List<Booking>? Bookings { get; set; }

        public int CupLevel { get; set; }

        public int CupLevelIndex { get; set; }

        public List<Event>? EventList { get; set; }

        public DateTime? FinishedDate { get; set; }

        public Team HomeTeam { get; set; }

        public List<Injury>? Injuries { get; set; }

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public MatchOfficials? MatchOfficials { get; set; }

        public int MatchRuleId { get; set; }

        public int MatchType { get; set; }

        public int? PossessionFirstHalfAway { get; set; }

        public int? PossessionFirstHalfHome { get; set; }

        public int? PossessionSecondHalfAway { get; set; }

        public int? PossessionSecondHalfHome { get; set; }

        public List<Goal>? Scorers { get; set; }
    }
}