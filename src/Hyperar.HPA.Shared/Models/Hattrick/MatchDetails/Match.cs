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

        public byte? AddedMinutes { get; set; }

        public Arena Arena { get; set; }

        public Team AwayTeam { get; set; }

        public List<Booking>? Bookings { get; set; }

        public byte CupLevel { get; set; }

        public byte CupLevelIndex { get; set; }

        public List<Event>? EventList { get; set; }

        public DateTime? FinishedDate { get; set; }

        public Team HomeTeam { get; set; }

        public List<Injury>? Injuries { get; set; }

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public MatchOfficials? MatchOfficials { get; set; }

        public byte MatchRuleId { get; set; }

        public byte MatchType { get; set; }

        public byte? PossessionFirstHalfAway { get; set; }

        public byte? PossessionFirstHalfHome { get; set; }

        public byte? PossessionSecondHalfAway { get; set; }

        public byte? PossessionSecondHalfHome { get; set; }

        public List<Goal>? Scorers { get; set; }
    }
}