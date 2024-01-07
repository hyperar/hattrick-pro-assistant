namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public abstract class HattrickEntityBase : IHattrickEntity
    {
        public uint HattrickId { get; set; }
    }
}