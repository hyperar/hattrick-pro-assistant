namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Goal
    {
        public Goal()
        {
            this.ScorerPlayerName = string.Empty;
        }

        public byte Index { get; set; }

        public byte MatchPart { get; set; }

        public byte ScorerAwayGoals { get; set; }

        public byte ScorerHomeGoals { get; set; }

        public byte ScorerMinute { get; set; }

        public long ScorerPlayerId { get; set; }

        public string ScorerPlayerName { get; set; }

        public long ScorerTeamId { get; set; }
    }
}