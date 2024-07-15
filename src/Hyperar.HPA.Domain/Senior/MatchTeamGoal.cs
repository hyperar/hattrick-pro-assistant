namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchTeamGoal : AuditableEntityBase, IAuditableEntity
    {
        public int AwayTeamScore { get; set; }

        public int HomeTeamScore { get; set; }

        public int Index { get; set; }

        public long MatchHattrickId { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeam MatchTeam { get; set; } = new MatchTeam();

        public int Minute { get; set; }

        public long PlayerHattrickId { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public long TeamHattrickId { get; set; }
    }
}