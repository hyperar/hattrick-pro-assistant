namespace Hyperar.HPA.Domain.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);

        void Update(TEntity entity);
    }
}