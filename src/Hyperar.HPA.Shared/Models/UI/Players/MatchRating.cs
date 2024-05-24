namespace Hyperar.HPA.Shared.Models.UI.Players
{
    using System;
    using System.Collections.Generic;
    using Shared.Enums;

    public class MatchRating
    {
        public MatchRating()
        {
            this.RatingStars = new List<string>();
        }

        public decimal AverageRating { get; set; }

        public DateTime Date { get; set; }

        public decimal EndOfMatchRating { get; set; }

        public List<string> RatingStars { get; set; }

        public MatchRole Role { get; set; }
    }
}