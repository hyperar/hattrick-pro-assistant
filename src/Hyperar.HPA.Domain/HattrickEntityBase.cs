namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    public class HattrickEntityBase : EntityBase, IEntity, IHattrickEntity
    {
        public uint HattrickId { get; set; }
    }
}