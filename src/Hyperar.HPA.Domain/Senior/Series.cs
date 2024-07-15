namespace Hyperar.HPA.Domain.Senior
{
    using System.Collections.Generic;
    using Domain.Interfaces;

    public class Series : AuditableEntityBase, IAuditableEntity
    {
        public int Level { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Rank { get; set; }

        public int Season { get; set; }

        public long SeriesHattrickId { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public virtual ICollection<SeriesTeam> Teams { get; set; } = new HashSet<SeriesTeam>();
    }
}