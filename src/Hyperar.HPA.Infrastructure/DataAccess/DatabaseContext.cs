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

        public void BeginTransaction()
        {
            if (this.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException("Cannot start a new transaction. \r\nThere is an active transaction already.");
            }

            this.Database.BeginTransaction();
        }

        public void Cancel()
        {
            this.cancelled = true;
        }

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void EndTransaction()
        {
            if (this.cancelled)
            {
                this.Rollback();
            }
            else
            {
                this.Commit();
            }
        }

        public void Migrate()
        {
            EnsureDatabaseInstanceIsReady();

            this.Database.Migrate();
        }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        private static void EnsureDatabaseInstanceIsReady()
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

            process.WaitForExit();
        }

        private void Commit()
        {
            this.SaveChanges();

            this.Database.CurrentTransaction?.Commit();
        }

        private void Rollback()
        {
            this.ChangeTracker.Clear();

            this.Database.CurrentTransaction?.Rollback();

            this.cancelled = false;

            this.cancelled = false;
        }
    }
}