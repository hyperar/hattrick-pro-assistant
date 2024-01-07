namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System;
    using System.Linq;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepositoryBase<TEntity>, IRepository<TEntity> where TEntity : EntityBase, IEntity
    {
        public Repository(IDatabaseContext context) : base(context)
        {
        }

        public async Task DeleteAsync(int id)
        {
            TEntity? entity = await this.GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            this.EntityCollection.Remove(entity);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await this.EntityCollection.Where(e => e.Id == id).SingleOrDefaultAsync();
        }
    }
}