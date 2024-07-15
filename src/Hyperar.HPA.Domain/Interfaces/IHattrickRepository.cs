namespace Hyperar.HPA.Domain.Interfaces
{
    using Domain;

    public interface IHattrickRepository<TEntity> : IAuditableRepository<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        Task DeleteAsync(long hattrickId);

        Task DeleteRangeAsync(ICollection<long> hattrickIds);

        Task<TEntity?> GetByHattrickIdAsync(long hattrickId);
    }
}