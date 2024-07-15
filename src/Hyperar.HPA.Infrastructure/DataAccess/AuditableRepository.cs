namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System.Linq.Expressions;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AuditableRepository<TEntity> : IAuditableRepository<TEntity> where TEntity : AuditableEntityBase, IAuditableEntity
    {
        private readonly IDatabaseContext context;

        public AuditableRepository(IDatabaseContext context)
        {
            this.context = context;
            this.EntityCollection = this.context.CreateSet<TEntity>();
        }

        protected DbSet<TEntity> EntityCollection { get; }

        public void Delete(TEntity entity)
        {
            this.EntityCollection.Remove(entity);
        }

        public void DeleteRange(ICollection<TEntity> entities)
        {
            this.EntityCollection.RemoveRange(entities);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;

            return (await this.EntityCollection.AddAsync(entity)).Entity;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return this.EntityCollection.Where(predicate ??= x => true);
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedOn = DateTime.Now;

            this.EntityCollection.Update(entity);
        }
    }
}