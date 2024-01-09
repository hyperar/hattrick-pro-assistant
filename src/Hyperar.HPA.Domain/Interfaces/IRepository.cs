namespace Hyperar.HPA.Domain.Interfaces
{
    using Domain;

    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase, IEntity
    {
        Task DeleteAsync(int id);

        Task DeleteRangeAsync(ICollection<int> ids);

        Task<TEntity?> GetByIdAsync(int id);
    }
}