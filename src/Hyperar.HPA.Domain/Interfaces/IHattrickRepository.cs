namespace Hyperar.HPA.Domain.Interfaces
{
    using Hyperar.HPA.Domain;

    public interface IHattrickRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        void Delete(long hattrickId);

        TEntity? GetByHattrickId(long hattrickId);
    }
}