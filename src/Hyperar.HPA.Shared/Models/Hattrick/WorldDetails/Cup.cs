namespace Hyperar.HPA.Shared.Models.Hattrick.WorldDetails
{
    public class Cup
    {
        public Cup()
        {
            this.CupName = string.Empty;
        }

        public long CupId { get; set; }

        public byte CupLeagueLevel { get; set; }

        public byte CupLevel { get; set; }

        public byte CupLevelIndex { get; set; }

        public string CupName { get; set; }

        public byte MatchRound { get; set; }

        public byte MatchRoundsLeft { get; set; }
    }
}