namespace Hyperar.HPA.Domain.Interfaces
{
    using Hyperar.HPA.Domain;

    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase, IEntity
    {
        void Delete(int id);

        TEntity? GetById(int id);
    }
}