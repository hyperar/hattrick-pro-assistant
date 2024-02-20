namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Common.Enums;

    public class Injury
    {
        public uint Index { get; set; }

        public uint InjuryMinute { get; set; }

        public uint InjuryPlayerId { get; set; }

        public string InjuryPlayerName { get; set; } = string.Empty;

        public uint InjuryTeamId { get; set; }

        public InjuryType InjuryType { get; set; }

        public MatchPart MatchPart { get; set; }
    }
}