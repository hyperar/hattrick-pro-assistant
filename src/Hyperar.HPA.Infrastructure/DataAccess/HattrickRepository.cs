namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System.Linq;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;

    public class HattrickRepository<TEntity> : RepositoryBase<TEntity>, IRepositoryBase<TEntity>, IHattrickRepository<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        public HattrickRepository(IDatabaseContext context) : base(context)
        {
        }

        public void Delete(long hattrickId)
        {
            TEntity? entity = this.GetByHattrickId(hattrickId);

            if (entity == null)
            {
                throw new Exception($"Entity of type \"{typeof(TEntity)}\" not found with Hattrick ID \"{hattrickId}\".");
            }

            this.EntityCollection.Remove(entity);
        }

        public TEntity? GetByHattrickId(long hattrickId)
        {
            return this.EntityCollection.Where(x => x.HattrickId == hattrickId).SingleOrDefault();
        }
    }
}