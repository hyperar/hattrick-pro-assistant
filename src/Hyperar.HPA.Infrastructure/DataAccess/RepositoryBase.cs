namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System;
    using System.Linq;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IDatabaseContext context;

        public RepositoryBase(IDatabaseContext context)
        {
            this.context = context;
            this.EntityCollection = this.context.CreateSet<TEntity>();
        }

        protected DbSet<TEntity> EntityCollection { get; private set; }

        public void Insert(TEntity entity)
        {
            this.EntityCollection.Add(entity);
        }

        public IQueryable<TEntity> Query(Func<TEntity, bool>? predicate = null)
        {
            IQueryable<TEntity> query = this.EntityCollection.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate).AsQueryable();
            }

            return query;
        }

        public void Update(TEntity entity)
        {
            this.EntityCollection.Update(entity);
        }
    }
}