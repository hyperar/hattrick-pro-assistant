namespace Hyperar.HPA.Application.Hattrick.WorldDetails
{
    public class Cup
    {
        public uint CupId { get; set; }

        public uint CupLeagueLevel { get; set; }

        public uint CupLevel { get; set; }

        public uint CupLevelIndex { get; set; }

        public string CupName { get; set; } = string.Empty;

        public uint MatchRound { get; set; }

        public uint MatchRoundsLeft { get; set; }
    }
}