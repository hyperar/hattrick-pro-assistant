namespace Hyperar.HPA.Application.Hattrick.Players
{
    using System;
    using Common.Enums;

    public class LastMatch
    {
        public DateTime Date { get; set; }

        public uint MatchId { get; set; }

        public uint PlayedMinutes { get; set; }

        public MatchRole PositionCode { get; set; }

        public decimal Rating { get; set; }

        public decimal RatingEndOfMatch { get; set; }
    }
}