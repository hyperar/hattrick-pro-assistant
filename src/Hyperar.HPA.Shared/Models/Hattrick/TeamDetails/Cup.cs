namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class Cup
    {
        public long? CupId { get; set; }

        public int? CupLeagueLevel { get; set; }

        public int? CupLeagueLevelIndex { get; set; }

        public int? CupLevel { get; set; }

        public string? CupName { get; set; }

        public int? MatchRound { get; set; }

        public int? MatchRoundsLeft { get; set; }

        public bool StillInCup { get; set; }
    }
}