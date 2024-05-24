namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class NextMatch
    {
        public NextMatch()
        {
            this.NextMatchAwayTeamName = string.Empty;
            this.NextMatchHomeTeamName = string.Empty;
        }

        public long NextMatchAwayTeamId { get; set; }

        public string NextMatchAwayTeamName { get; set; }

        public DateTime NextMatchDate { get; set; }

        public long NextMatchHomeTeamId { get; set; }

        public string NextMatchHomeTeamName { get; set; }

        public long NextMatchId { get; set; }
    }
}