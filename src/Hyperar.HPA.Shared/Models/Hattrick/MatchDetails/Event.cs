namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Event
    {
        public Event()
        {
            this.EventText = string.Empty;
        }

        public string EventText { get; set; }

        public short EventTypeId { get; set; }

        public short EventVariation { get; set; }

        public byte Index { get; set; }

        public byte MatchPart { get; set; }

        public byte Minute { get; set; }

        public long ObjectPlayerId { get; set; }

        public long SubjectPlayerId { get; set; }

        public long SubjectTeamId { get; set; }
    }
}