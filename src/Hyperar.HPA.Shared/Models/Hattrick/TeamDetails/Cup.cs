namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class Cup
    {
        public long? CupId { get; set; }

        public byte? CupLeagueLevel { get; set; }

        public byte? CupLeagueLevelIndex { get; set; }

        public byte? CupLevel { get; set; }

        public string? CupName { get; set; }

        public byte? MatchRound { get; set; }

        public byte? MatchRoundsLeft { get; set; }

        public bool StillInCup { get; set; }
    }
}