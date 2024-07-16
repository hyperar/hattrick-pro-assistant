namespace Hyperar.HPA.Domain.Junior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchEvent : AuditableEntityBase, IAuditableEntity
    {
        public int Index { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public long MatchHattrickId { get; set; }

        public MatchPart MatchPart { get; set; }

        public int Minute { get; set; }

        public long? ObjectPlayerHattrickId { get; set; }

        public long? SubjectPlayerHattrickId { get; set; }

        public long? SubjectTeamHattrickId { get; set; }

        public string? Text { get; set; }

        public int Type { get; set; }

        public int Variation { get; set; }
    }
}