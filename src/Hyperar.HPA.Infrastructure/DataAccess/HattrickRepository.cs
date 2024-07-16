namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System.Linq;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HattrickRepository<TEntity> : AuditableRepository<TEntity>, IAuditableRepository<TEntity>, IHattrickRepository<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        public HattrickRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task DeleteAsync(long hattrickId)
        {
            TEntity? entity = await this.GetByHattrickIdAsync(hattrickId);

            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            this.EntityCollection.Remove(entity);
        }

        public async Task DeleteRangeAsync(ICollection<long> hattrickIds)
        {
            ICollection<TEntity> entities = await this.EntityCollection.Where(x => hattrickIds.Contains(x.HattrickId)).ToListAsync();

            if (entities.Count != hattrickIds.Count)
            {
                throw new ArgumentException(nameof(entities));
            }

            this.EntityCollection.RemoveRange(entities);
        }

        public async Task<TEntity?> GetByHattrickIdAsync(long hattrickId)
        {
            return await this.EntityCollection.Where(x => x.HattrickId == hattrickId).SingleOrDefaultAsync();
        }
    }
}