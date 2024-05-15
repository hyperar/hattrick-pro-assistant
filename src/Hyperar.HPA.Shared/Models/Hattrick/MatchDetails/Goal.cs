namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Goal
    {
        public Goal()
        {
            this.ScorerPlayerName = string.Empty;
        }

        public int Index { get; set; }

        public int MatchPart { get; set; }

        public int ScorerAwayGoals { get; set; }

        public int ScorerHomeGoals { get; set; }

        public int ScorerMinute { get; set; }

        public long ScorerPlayerId { get; set; }

        public string ScorerPlayerName { get; set; }

        public long ScorerTeamId { get; set; }
    }
}