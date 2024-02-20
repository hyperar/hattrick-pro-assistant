namespace Hyperar.HPA.Domain.Senior
{
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchTeamGoal : EntityBase, IEntity
    {
        public uint AwayTeamScore { get; set; }

        public uint HomeTeamScore { get; set; }

        public uint Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public uint Minute { get; set; }

        public uint PlayerHattrickId { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public virtual MatchTeam Team { get; set; } = new MatchTeam();
    }
}