namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    using System;

    public class Match
    {
        public int? AwayGoals { get; set; }

        public AwayTeam AwayTeam { get; set; } = new AwayTeam();

        public int CupLevel { get; set; }

        public int CupLevelIndex { get; set; }

        public int? HomeGoals { get; set; }

        public HomeTeam HomeTeam { get; set; } = new HomeTeam();

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public int MatchType { get; set; }

        public bool? OrdersGiven { get; set; }

        public string SourceSystem { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}