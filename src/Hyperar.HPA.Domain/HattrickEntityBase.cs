namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public abstract class HattrickEntityBase : AuditableEntityBase, IHattrickEntity, IAuditableEntity
    {
        public long HattrickId { get; set; }
    }
}