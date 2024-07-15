namespace Hyperar.HPA.Domain.Interfaces
{
    public interface IEntity : IAuditableEntity
    {
        int Id { get; set; }
    }
}