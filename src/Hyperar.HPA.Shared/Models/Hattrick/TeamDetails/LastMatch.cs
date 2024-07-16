namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class LastMatch
    {
        public int LastMatchAwayGoals { get; set; }

        public long LastMatchAwayTeamId { get; set; }

        public string LastMatchAwayTeamName { get; set; } = string.Empty;

        public DateTime LastMatchDate { get; set; }

        public int LastMatchHomeGoals { get; set; }

        public long LastMatchHomeTeamId { get; set; }

        public string LastMatchHomeTeamName { get; set; } = string.Empty;

        public long LastMatchId { get; set; }
    }
}