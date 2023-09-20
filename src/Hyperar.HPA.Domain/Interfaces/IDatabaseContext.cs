namespace Hyperar.HPA.Domain.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    public interface IDatabaseContext
    {
        void BeginTransaction();

        void Cancel();

        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        void EndTransaction();

        void Migrate();

        void Save();
    }
}