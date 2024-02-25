namespace Hyperar.HPA.Domain.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    public interface IDatabaseContext
    {
        Task BeginTransactionAsync();

        void Cancel();

        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        Task EndTransactionAsync();

        Task MigrateAsync();

        Task SaveAsync();
    }
}