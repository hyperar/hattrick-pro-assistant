namespace Hyperar.HPA.Shared.Models.Hattrick.MatchArchive
{
    using System;

    public class Match
    {
        public Match()
        {
            this.AwayTeam = new AwayTeam();
            this.HomeTeam = new HomeTeam();
            this.SourceSystem = string.Empty;
        }

        public int? AwayGoals { get; set; }

        public AwayTeam AwayTeam { get; set; }

        public long CupId { get; set; }

        public int CupLevel { get; set; }

        public int CupLevelIndex { get; set; }

        public int? HomeGoals { get; set; }

        public HomeTeam HomeTeam { get; set; }

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public int MatchRuleId { get; set; }

        public int MatchType { get; set; }

        public string SourceSystem { get; set; }
    }
}