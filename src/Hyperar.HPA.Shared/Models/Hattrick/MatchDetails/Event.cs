namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Event
    {
        public Event()
        {
            this.EventText = string.Empty;
        }

        public string EventText { get; set; }

        public int EventTypeId { get; set; }

        public int EventVariation { get; set; }

        public int Index { get; set; }

        public int MatchPart { get; set; }

        public int Minute { get; set; }

        public long ObjectPlayerId { get; set; }

        public long SubjectPlayerId { get; set; }

        public long SubjectTeamId { get; set; }
    }
}