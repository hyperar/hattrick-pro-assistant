namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Common.Enums;

    public class Goal
    {
        public uint Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public uint ScorerAwayGoals { get; set; }

        public uint ScorerHomeGoals { get; set; }

        public uint ScorerMinute { get; set; }

        public uint ScorerPlayerId { get; set; }

        public string ScorerPlayerName { get; set; } = string.Empty;

        public uint ScorerTeamId { get; set; }
    }
}