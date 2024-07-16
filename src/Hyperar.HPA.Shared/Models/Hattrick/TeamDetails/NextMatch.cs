namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class NextMatch
    {
        public long NextMatchAwayTeamId { get; set; }

        public string NextMatchAwayTeamName { get; set; } = string.Empty;

        public DateTime NextMatchDate { get; set; }

        public long NextMatchHomeTeamId { get; set; }

        public string NextMatchHomeTeamName { get; set; } = string.Empty;

        public long NextMatchId { get; set; }
    }
}