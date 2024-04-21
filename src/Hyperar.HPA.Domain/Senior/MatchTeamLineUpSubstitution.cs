namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchLineUp;

    public class MatchTeamLineUpSubstitution : EntityBase, IEntity
    {
        public MatchTeamLineUpSubstitution()
        {
            this.MatchTeamLineUp = new MatchTeamLineUp();
        }

        public long InPlayerHattrickId { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeamLineUp MatchTeamLineUp { get; set; }

        public int MatchTeamLineUpId { get; set; }

        public byte Minute { get; set; }

        public MatchRole NewRole { get; set; }

        public MatchRoleBehavior NewRoleBehavior { get; set; }

        public MatchOrderType OrderType { get; set; }

        public long OutPlayerHattrickId { get; set; }

        public static MatchTeamLineUpSubstitution Create(Models.Substitution xmlSubstitution, MatchTeamLineUp matchTeamLineUp)
        {
            return new MatchTeamLineUpSubstitution
            {
                InPlayerHattrickId = xmlSubstitution.ObjectPlayerId,
                MatchPart = (MatchPart)xmlSubstitution.MatchPart,
                MatchTeamLineUp = matchTeamLineUp,
                Minute = xmlSubstitution.MatchMinute,
                NewRole = (MatchRole)xmlSubstitution.NewPositionId,
                NewRoleBehavior = (MatchRoleBehavior)xmlSubstitution.NewPositionBehaviour,
                OrderType = (MatchOrderType)xmlSubstitution.OrderType,
                OutPlayerHattrickId = xmlSubstitution.SubjectPlayerId
            };
        }
    }
}