namespace Hyperar.HPA.Domain.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IAuditableRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);

        void DeleteRange(ICollection<TEntity> entities);

        Task<TEntity> InsertAsync(TEntity entity);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);

        void Update(TEntity entity);
    }
}