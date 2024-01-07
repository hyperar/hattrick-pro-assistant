namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
    }
}