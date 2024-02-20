namespace Hyperar.HPA.Application.Hattrick.MatchLineUp
{
    using Common.Enums;

    public class Substitution
    {
        public uint MatchMinute { get; set; }

        public MatchPart MatchPart { get; set; }

        public MatchRoleBehavior NewPositionBehavior { get; set; }

        public ushort NewPositionId { get; set; }

        public uint ObjectPlayerId { get; set; }

        public MatchOrderType OrderType { get; set; }

        public uint SubjectPlayerId { get; set; }

        public uint TeamId { get; set; }
    }
}