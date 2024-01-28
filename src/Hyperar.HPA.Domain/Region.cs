namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class Region : HattrickEntityBase, IHattrickEntity
    {
        public virtual Country Country { get; set; } = new Country();

        public uint CountryHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}