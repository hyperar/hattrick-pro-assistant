namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System;
    using System.Linq;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepositoryBase<TEntity>, IRepository<TEntity> where TEntity : EntityBase, IEntity
    {
        public Repository(IDatabaseContext context) : base(context)
        {
        }

        public async Task DeleteAsync(int id)
        {
            TEntity? entity = await this.GetByIdAsync(id);

            if (entity != null)
            {
                this.EntityCollection.Remove(entity);
            }
            else
            {
                throw new Exception($"Entity of type \"{typeof(TEntity)}\" not found with ID \"{id}\".");
            }
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await this.EntityCollection.Where(e => e.Id == id).SingleOrDefaultAsync();
        }
    }
}