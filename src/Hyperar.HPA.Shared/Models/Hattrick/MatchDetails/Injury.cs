namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Injury
    {
        public Injury()
        {
            this.InjuryPlayerName = string.Empty;
        }

        public int Index { get; set; }

        public int InjuryMinute { get; set; }

        public long InjuryPlayerId { get; set; }

        public string InjuryPlayerName { get; set; }

        public long InjuryTeamId { get; set; }

        public int InjuryType { get; set; }

        public int MatchPart { get; set; }
    }
}