namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    public class Substitution
    {
        public byte MatchMinute { get; set; }

        public byte MatchPart { get; set; }

        public short NewPositionBehaviour { get; set; }

        public short NewPositionId { get; set; }

        public long ObjectPlayerId { get; set; }

        public byte OrderType { get; set; }

        public long SubjectPlayerId { get; set; }

        public long TeamId { get; set; }
    }
}