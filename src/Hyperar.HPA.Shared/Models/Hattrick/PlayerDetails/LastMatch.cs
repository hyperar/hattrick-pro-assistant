namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    using System;

    public class LastMatch
    {
        public DateTime Date { get; set; }

        public long MatchId { get; set; }

        public int PlayedMinutes { get; set; }

        public int PositionCode { get; set; }

        public decimal Rating { get; set; }

        public decimal RatingEndOfMatch { get; set; }
    }
}