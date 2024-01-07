namespace Hyperar.HPA.Domain.Interfaces
{
    using Domain;

    public interface IHattrickRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        Task DeleteAsync(long hattrickId);

        Task<TEntity?> GetByHattrickIdAsync(long hattrickId);
    }
}