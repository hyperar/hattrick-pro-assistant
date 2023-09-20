namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
    }
}