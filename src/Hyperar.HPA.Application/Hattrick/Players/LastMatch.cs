namespace Hyperar.HPA.Application.Hattrick.Players
{
    using System;

    public class LastMatch
    {
        public DateTime Date { get; set; }

        public uint MatchId { get; set; }

        public uint PlayedMinutes { get; set; }

        public ushort PositionCode { get; set; }

        public decimal Rating { get; set; }

        public decimal RatingEndOfMatch { get; set; }
    }
}