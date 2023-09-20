namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    public class Cup
    {
        public uint? CupId { get; set; }

        public uint? CupLeagueLevel { get; set; }

        public uint? CupLeagueLevelIndex { get; set; }

        public uint? CupLevel { get; set; }

        public string? CupName { get; set; }

        public uint? MatchRound { get; set; }

        public uint? MatchRoundsLeft { get; set; }

        public bool StillInCup { get; set; }
    }
}