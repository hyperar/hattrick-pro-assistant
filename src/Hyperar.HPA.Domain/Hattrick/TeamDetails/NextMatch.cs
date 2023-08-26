namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;

    public class NextMatch
    {
        public uint NextMatchId { get; set; }

        public DateTime NextMatchDate { get; set; }

        public uint NextMatchHomeTeamId { get; set; }

        public string NextMatchHomeTeamName { get; set; } = string.Empty;

        public uint NextMatchAwayTeamId { get; set; }

        public string NextMatchAwayTeamName { get; set; } = string.Empty;
    }
}
