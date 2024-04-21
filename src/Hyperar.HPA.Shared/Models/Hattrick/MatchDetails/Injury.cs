namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Injury
    {
        public Injury()
        {
            this.InjuryPlayerName = string.Empty;
        }

        public byte Index { get; set; }

        public byte InjuryMinute { get; set; }

        public long InjuryPlayerId { get; set; }

        public string InjuryPlayerName { get; set; }

        public long InjuryTeamId { get; set; }

        public byte InjuryType { get; set; }

        public byte MatchPart { get; set; }
    }
}