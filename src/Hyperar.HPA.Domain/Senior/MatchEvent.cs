namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchEvent : EntityBase, IEntity
    {
        public MatchEvent()
        {
            this.Match = new Match();
        }

        public long? AdditionalPlayerHattrickId { get; set; }

        public int Index { get; set; }

        public virtual Match Match { get; set; }

        public long MatchHattrickId { get; set; }

        public MatchPart MatchPart { get; set; }

        public int Minute { get; set; }

        public long? PlayerHattrickId { get; set; }

        public long? TeamHattrickId { get; set; }

        public string? Text { get; set; }

        public int Type { get; set; }

        public int Variation { get; set; }

        public static MatchEvent Create(Models.Event xmlEvent, Match match)
        {
            return new MatchEvent
            {
                AdditionalPlayerHattrickId = xmlEvent.ObjectPlayerId != 0 ? xmlEvent.ObjectPlayerId : null,
                Index = xmlEvent.Index,
                Match = match,
                MatchPart = (MatchPart)xmlEvent.MatchPart,
                Minute = xmlEvent.Minute,
                PlayerHattrickId = xmlEvent.SubjectPlayerId != 0 ? xmlEvent.SubjectPlayerId : null,
                TeamHattrickId = xmlEvent.SubjectTeamId != 0 ? xmlEvent.SubjectTeamId : null,
                Text = xmlEvent.EventText,
                Type = xmlEvent.EventTypeId,
                Variation = xmlEvent.EventVariation
            };
        }

        public bool HasChanged(Models.Event xmlEvent)
        {
            return this.Text != xmlEvent.EventText;
        }

        public void Update(Models.Event xmlEvent)
        {
            this.Text = xmlEvent.EventText;
        }
    }
}