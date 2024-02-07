namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Hyperar.HPA.Common.Enums;

    public class Event
    {
        public string EventText { get; set; } = string.Empty;

        public uint EventTypeId { get; set; }

        public uint EventVariation { get; set; }

        public uint Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public uint Minute { get; set; }

        public uint ObjectPlayerId { get; set; }

        public uint SubjectPlayerId { get; set; }

        public uint SubjectTeamId { get; set; }
    }
}