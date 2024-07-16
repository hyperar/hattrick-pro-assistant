namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchTeamLineUpSubstitution : AuditableEntityBase, IAuditableEntity
    {
        public long InPlayerHattrickId { get; set; }

        public long MatchHattrickId { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeamLineUp MatchTeamLineUp { get; set; } = new MatchTeamLineUp();

        public int Minute { get; set; }

        public MatchRoleBehavior NewBehavior { get; set; }

        public MatchRole NewRole { get; set; }

        public long OutPlayerHattrickId { get; set; }

        public long TeamHattrickId { get; set; }

        public MatchOrderType Type { get; set; }
    }
}