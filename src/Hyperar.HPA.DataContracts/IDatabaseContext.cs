namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public interface IDatabaseContext
    {
        void BeginTransaction();

        void Cancel();

        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class, IEntity;

        void EndTransaction();

        void Save();
    }
}