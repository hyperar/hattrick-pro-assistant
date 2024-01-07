namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System;
    using System.Diagnostics;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private const string sqlCommandName = "sqllocaldb.exe";

        private const string sqlCommandParameters = "c HPA -s";

        private bool cancelled;

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            this.cancelled = false;
        }

        public async Task BeginTransactionAsync()
        {
            if (this.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException(nameof(this.BeginTransactionAsync));
            }

            await this.Database.BeginTransactionAsync();
        }

        public void Cancel()
        {
            this.cancelled = true;
        }

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public async Task EndTransactionAsync()
        {
            if (this.cancelled)
            {
                await this.RollbackAsync();
            }
            else
            {
                await this.CommitAsync();
            }
        }

        public async Task MigrateAsync()
        {
            await EnsureDatabaseInstanceIsReadyAsync();

            await this.Database.MigrateAsync();
        }

        public async Task SaveAsync()
        {
            await this.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        private static async Task EnsureDatabaseInstanceIsReadyAsync()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = sqlCommandParameters, // Creates and starts the instance.
                    CreateNoWindow = true,
                    FileName = sqlCommandName,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            };

            process.Start();

            await process.WaitForExitAsync();
        }

        private async Task CommitAsync()
        {
            await this.SaveChangesAsync();

            if (this.Database.CurrentTransaction != null)
            {
                await this.Database.CurrentTransaction.CommitAsync();
            }
        }

        private async Task RollbackAsync()
        {
            this.ChangeTracker.Clear();

            if (this.Database.CurrentTransaction != null)
            {
                await this.Database.CurrentTransaction.RollbackAsync();
            }

            this.cancelled = false;
        }
    }
}