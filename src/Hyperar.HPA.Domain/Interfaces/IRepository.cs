namespace Hyperar.HPA.Domain.Interfaces
{
    using Hyperar.HPA.Domain;

    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase, IEntity
    {
        Task DeleteAsync(int id);

        Task<TEntity?> GetByIdAsync(int id);
    }
}