namespace Hyperar.HPA.Domain.Interfaces
{
    using System;
    using System.Linq;

    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        IQueryable<TEntity> Query(Func<TEntity, bool>? predicate = null);

        void Update(TEntity entity);
    }
}