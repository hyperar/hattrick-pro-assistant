namespace Hyperar.HPA.Shared.Models.Hattrick.WorldDetails
{
    public class Cup
    {
        public long CupId { get; set; }

        public int CupLeagueLevel { get; set; }

        public int CupLevel { get; set; }

        public int CupLevelIndex { get; set; }

        public string CupName { get; set; } = string.Empty;

        public int MatchRound { get; set; }

        public int MatchRoundsLeft { get; set; }
    }
}