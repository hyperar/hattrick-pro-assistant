namespace Hyperar.HPA.Application.Hattrick.Matches
{
    using System;
    using Common.Enums;

    public class Match
    {
        public uint? AwayGoals { get; set; }

        public AwayTeam AwayTeam { get; set; } = new AwayTeam();

        public uint CupLevel { get; set; }

        public uint CupLevelIndex { get; set; }

        public uint? HomeGoals { get; set; }

        public HomeTeam HomeTeam { get; set; } = new HomeTeam();

        public uint MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public uint MatchId { get; set; }

        public MatchType MatchType { get; set; }

        public bool? OrdersGiven { get; set; }

        public string SourceSystem { get; set; } = string.Empty;

        public MatchStatus Status { get; set; }
    }
}