namespace Hyperar.HPA.Domain.Senior
{
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchTeamLineUpSubstitution : EntityBase, IEntity
    {
        public uint InPlayerHattrickId { get; set; }

        public virtual MatchTeamLineUp LineUp { get; set; } = new MatchTeamLineUp();

        public MatchPart MatchPart { get; set; }

        public uint Minute { get; set; }

        public ushort NewRole { get; set; }

        public MatchRoleBehavior NewRoleBehavior { get; set; }

        public MatchOrderType OrderType { get; set; }

        public uint OutPlayerHattrickId { get; set; }
    }
}