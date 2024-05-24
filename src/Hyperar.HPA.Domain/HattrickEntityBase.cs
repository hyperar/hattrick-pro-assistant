namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public abstract class HattrickEntityBase : IHattrickEntity
    {
        public long HattrickId { get; set; }
    }
}