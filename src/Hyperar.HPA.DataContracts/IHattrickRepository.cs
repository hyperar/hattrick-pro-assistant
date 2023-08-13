namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain.Interfaces;

    public interface IHattrickRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, IHattrickEntity
    {
        bool CheckIfExistsByHattrickId(uint hattrickId);

        TEntity? GetByHattrickId(long hattrickId);
    }
}