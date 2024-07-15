namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class Region : HattrickEntityBase, IHattrickEntity
    {
        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Senior.Team> SeniorTeams { get; set; } = new HashSet<Senior.Team>();
    }
}