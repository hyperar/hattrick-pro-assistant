namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System;

    public class NextMatch
    {
        public uint NextMatchAwayTeamId { get; set; }

        public string NextMatchAwayTeamName { get; set; } = string.Empty;

        public DateTime NextMatchDate { get; set; }

        public uint NextMatchHomeTeamId { get; set; }

        public string NextMatchHomeTeamName { get; set; } = string.Empty;

        public uint NextMatchId { get; set; }
    }
}