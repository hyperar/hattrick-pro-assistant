namespace Hyperar.HPA.Shared.Models.Hattrick.Players
{
    using System;

    public class LastMatch
    {
        public DateTime Date { get; set; }

        public long MatchId { get; set; }

        public byte PlayedMinutes { get; set; }

        public short PositionCode { get; set; }

        public decimal Rating { get; set; }

        public decimal RatingEndOfMatch { get; set; }
    }
}