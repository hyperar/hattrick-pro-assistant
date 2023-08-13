namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    public class Country : HattrickEntityBase, IEntity, IHattrickEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}