namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;
    using Domain.Senior;
    using Hattrick = Shared.Models.Hattrick;

    public class Region : HattrickEntityBase, IHattrickEntity
    {
        public Region()
        {
            this.Country = new Country();
            this.Teams = new HashSet<Team>();

            this.Name = string.Empty;
        }

        public virtual Country Country { get; set; }

        public long CountryHattrickId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public static Region Create(Hattrick.IdName xmlRegion, Country country)
        {
            return new Region
            {
                Country = country,
                HattrickId = xmlRegion.Id,
                Name = xmlRegion.Name
            };
        }

        public bool HasChanged(Hattrick.IdName xmlRegion)
        {
            return this.Name != xmlRegion.Name;
        }

        public void Update(Hattrick.IdName xmlRegion)
        {
            this.Name = xmlRegion.Name;
        }
    }
}