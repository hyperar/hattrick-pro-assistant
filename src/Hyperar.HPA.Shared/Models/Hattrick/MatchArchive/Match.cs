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

        public byte? AwayGoals { get; set; }

        public AwayTeam AwayTeam { get; set; }

        public byte CupLevel { get; set; }

        public byte CupLevelIndex { get; set; }

        public byte? HomeGoals { get; set; }

        public HomeTeam HomeTeam { get; set; }

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public byte MatchType { get; set; }

        public string SourceSystem { get; set; }

        public byte MatchRuleId { get; set; }

        public long CupId { get; set; }
    }
}