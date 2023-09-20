namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    public abstract class HattrickEntityBase : IHattrickEntity
    {
        public uint HattrickId { get; set; }
    }
}