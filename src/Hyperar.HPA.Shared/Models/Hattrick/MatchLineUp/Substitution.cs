namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    public class Substitution
    {
        public int MatchMinute { get; set; }

        public int MatchPart { get; set; }

        public int NewPositionBehaviour { get; set; }

        public int NewPositionId { get; set; }

        public long ObjectPlayerId { get; set; }

        public int OrderType { get; set; }

        public long SubjectPlayerId { get; set; }

        public long TeamId { get; set; }
    }
}