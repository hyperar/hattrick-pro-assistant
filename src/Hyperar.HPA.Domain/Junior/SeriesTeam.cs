namespace Hyperar.HPA.Domain.Junior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class SeriesTeam : AuditableEntityBase, IAuditableEntity
    {
        public SeriesPositionChange Change { get; set; }

        public int DrawnMatches { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalsFor { get; set; }

        public long HattrickId { get; set; }

        public int LostMatches { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Points { get; set; }

        public int Position { get; set; }

        public int Season { get; set; }

        public virtual Series Series { get; set; } = new Series();

        public long SeriesHattrickId { get; set; }

        public long TeamHattrickId { get; set; }

        public int Week { get; set; }

        public int WonMatches { get; set; }
    }
}