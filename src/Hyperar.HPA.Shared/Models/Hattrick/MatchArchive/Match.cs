namespace Hyperar.HPA.Shared.Models.Hattrick.MatchArchive
{
    using System;

    public class Match
    {
        public int? AwayGoals { get; set; }

        public IdName AwayTeam { get; set; } = new IdName();

        public long CupId { get; set; }

        public int CupLevel { get; set; }

        public int CupLevelIndex { get; set; }

        public int? HomeGoals { get; set; }

        public IdName HomeTeam { get; set; } = new IdName();

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public int MatchRuleId { get; set; }

        public int MatchType { get; set; }

        public string SourceSystem { get; set; } = string.Empty;
    }
}