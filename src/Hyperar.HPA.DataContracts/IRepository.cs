namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain.Interfaces;

    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        bool CheckIfExistsById(int id);

        void Delete(int id);

        TEntity? GetById(int id);

        void Insert(TEntity entity);

        IQueryable<TEntity> Query(Func<TEntity, bool>? predicate = null);

        void Update(TEntity entity);
    }
}