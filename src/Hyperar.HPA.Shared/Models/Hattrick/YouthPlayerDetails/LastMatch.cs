namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerDetails
{
    using System;

    public class LastMatch
    {
        public DateTime Date { get; set; }

        public int PlayedMinutes { get; set; }

        public int PositionCode { get; set; }

        public decimal Rating { get; set; }

        public long YouthMatchId { get; set; }
    }
}