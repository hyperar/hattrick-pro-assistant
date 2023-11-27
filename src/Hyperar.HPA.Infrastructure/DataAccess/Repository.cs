namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System;
    using System.Linq;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;

    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepositoryBase<TEntity>, IRepository<TEntity> where TEntity : EntityBase, IEntity
    {
        public Repository(IDatabaseContext context) : base(context)
        {
        }

        public void Delete(int id)
        {
            TEntity? entity = this.GetById(id);

            if (entity != null)
            {
                this.EntityCollection.Remove(entity);
            }
            else
            {
                throw new Exception($"Entity of type \"{typeof(TEntity)}\" not found with ID \"{id}\".");
            }
        }

        public TEntity? GetById(int id)
        {
            return this.EntityCollection.Where(e => e.Id == id).SingleOrDefault();
        }
    }
}