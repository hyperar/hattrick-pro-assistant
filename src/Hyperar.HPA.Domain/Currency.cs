namespace Hyperar.HPA.Domain
{
    using System.Collections.Generic;
    using Domain.Interfaces;

    public class Currency : EntityBase, IEntity
    {
        public virtual ICollection<Country> Countries { get; set; } = new HashSet<Country>();

        public string Name { get; set; } = string.Empty;

        public decimal Rate { get; set; }
    }
}