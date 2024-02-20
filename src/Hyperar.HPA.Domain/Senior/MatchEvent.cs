namespace Hyperar.HPA.Domain.Senior
{
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchEvent : EntityBase, IEntity
    {
        public uint? AdditionalPlayerHattrickId { get; set; }

        public uint Index { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public MatchPart MatchPart { get; set; }

        public uint Minute { get; set; }

        public uint? PlayerHattrickId { get; set; }

        public uint? TeamHattrickId { get; set; }

        public string? Text { get; set; }

        public uint Type { get; set; }

        public uint Variation { get; set; }
    }
}