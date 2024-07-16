namespace Hyperar.HPA.Domain.Interfaces
{
    public interface IHattrickEntity : IAuditableEntity
    {
        long HattrickId { get; set; }
    }
}