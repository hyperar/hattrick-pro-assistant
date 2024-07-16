namespace Hyperar.HPA.Domain.Junior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchArena : AuditableEntityBase, IAuditableEntity
    {
        public long ArenaHattrickId { get; set; }

        public int AttendanceBasic { get; set; }

        public int AttendanceRoof { get; set; }

        public int AttendanceTerraces { get; set; }

        public int AttendanceTotal { get; set; }

        public int AttendanceVip { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public long MatchHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public Weather Weather { get; set; }
    }
}