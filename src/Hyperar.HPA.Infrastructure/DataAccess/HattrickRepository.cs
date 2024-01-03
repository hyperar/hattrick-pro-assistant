namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System.Linq;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HattrickRepository<TEntity> : RepositoryBase<TEntity>, IRepositoryBase<TEntity>, IHattrickRepository<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        public HattrickRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task DeleteAsync(long hattrickId)
        {
            TEntity? entity = await this.GetByHattrickIdAsync(hattrickId);

            if (entity != null)
            {
                this.EntityCollection.Remove(entity);
            }
            else
            {
                throw new Exception($"Entity of type \"{typeof(TEntity)}\" not found with Hattrick ID \"{hattrickId}\".");
            }
        }

        public async Task<TEntity?> GetByHattrickIdAsync(long hattrickId)
        {
            return await this.EntityCollection.Where(x => x.HattrickId == hattrickId).SingleOrDefaultAsync();
        }
    }
}