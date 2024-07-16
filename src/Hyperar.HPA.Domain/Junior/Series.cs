namespace Hyperar.HPA.Domain.Junior
{
    using System.Collections.Generic;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Series : AuditableEntityBase, IAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public int Season { get; set; }

        public long SeriesHattrickId { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public virtual ICollection<SeriesTeam> Teams { get; set; } = new HashSet<SeriesTeam>();

        public YouthLeagueType Type { get; set; }
    }
}