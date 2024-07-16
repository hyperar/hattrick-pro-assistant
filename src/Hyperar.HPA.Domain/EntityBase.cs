namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public abstract class EntityBase : AuditableEntityBase, IEntity, IAuditableEntity
    {
        public int Id { get; set; }
    }
}