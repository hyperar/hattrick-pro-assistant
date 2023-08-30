namespace Hyperar.HPA.Domain.Database
{
    using Hyperar.HPA.Domain.Interfaces;

    public class Region : HattrickEntityBase, IEntity, IHattrickEntity
    {
        public string Name { get; set; } = string.Empty;

        public int CountryId { get; set; }

        public virtual Country? Country { get; set; }
    }
}
