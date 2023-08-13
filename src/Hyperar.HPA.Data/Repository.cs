namespace Hyperar.HPA.Data
{
    using System;
    using System.Linq;
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, Domain.Interfaces.IEntity
    {
        private readonly IDatabaseContext context;

        public Repository(IDatabaseContext context)
        {
            this.context = context;
            this.EntityCollection = this.context.CreateSet<TEntity>();
        }

        protected DbSet<TEntity> EntityCollection { get; private set; }

        public bool CheckIfExistsById(int id)
        {
            return this.EntityCollection.Any(e => e.Id == id);
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id) ?? throw new ArgumentNullException(nameof(id));

            this.context.CreateSet<TEntity>().Remove(entity);
        }

        public TEntity? GetById(int id)
        {
            return this.EntityCollection
                       .Where(e => e.Id == id)
                       .SingleOrDefault();
        }

        public void Insert(TEntity entity)
        {
            this.EntityCollection.Add(entity);
        }

        public IQueryable<TEntity> Query(Func<TEntity, bool>? predicate = null)
        {
            var query = this.EntityCollection.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate)
                             .AsQueryable();
            }

            return query;
        }

        public void Update(TEntity entity)
        {
            this.EntityCollection.Update(entity);
        }
    }
}