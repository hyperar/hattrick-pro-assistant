namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System;

    public class LastMatch
    {
        public uint LastMatchAwayGoals { get; set; }

        public uint LastMatchAwayTeamId { get; set; }

        public string LastMatchAwayTeamName { get; set; } = string.Empty;

        public DateTime LastMatchDate { get; set; }

        public uint LastMatchHomeGoals { get; set; }

        public uint LastMatchHomeTeamId { get; set; }

        public string LastMatchHomeTeamName { get; set; } = string.Empty;

        public uint LastMatchId { get; set; }
    }
}