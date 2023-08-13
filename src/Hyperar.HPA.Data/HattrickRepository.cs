namespace Hyperar.HPA.Data
{
    using System.Linq;
    using Hyperar.HPA.DataContracts;

    public class HattrickRepository<TEntity> : Repository<TEntity>, IHattrickRepository<TEntity> where TEntity : class, Domain.Interfaces.IEntity, Domain.Interfaces.IHattrickEntity
    {
        public HattrickRepository(IDatabaseContext context) : base(context)
        {
        }

        public bool CheckIfExistsByHattrickId(uint hattrickId)
        {
            return this.EntityCollection.Any(e => e.HattrickId == hattrickId);
        }

        public TEntity? GetByHattrickId(long hattrickId)
        {
            return this.EntityCollection.SingleOrDefault(e => e.HattrickId == hattrickId);
        }
    }
}