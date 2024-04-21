namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class LastMatch
    {
        public LastMatch()
        {
            this.LastMatchAwayTeamName = string.Empty;
            this.LastMatchHomeTeamName = string.Empty;
        }

        public byte LastMatchAwayGoals { get; set; }

        public long LastMatchAwayTeamId { get; set; }

        public string LastMatchAwayTeamName { get; set; }

        public DateTime LastMatchDate { get; set; }

        public byte LastMatchHomeGoals { get; set; }

        public long LastMatchHomeTeamId { get; set; }

        public string LastMatchHomeTeamName { get; set; }

        public long LastMatchId { get; set; }
    }
}