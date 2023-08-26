namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    public class Cup
    {
        public bool StillInCup { get; set; }

        public uint? CupId { get; set; }

        public string? CupName { get; set; }

        public uint? CupLeagueLevel { get; set; }

        public uint? CupLevel { get; set; }

        public uint? CupLeagueLevelIndex { get; set; }

        public uint? MatchRound { get; set; }

        public uint? MatchRoundsLeft { get; set; }
    }
}
