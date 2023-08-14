namespace Hyperar.HPA.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        private bool cancelled;

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public void BeginTransaction()
        {
            if (this.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException();
            }

            this.Database.BeginTransaction();
        }

        public void Cancel()
        {
            this.cancelled = true;
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

        public void Save()
        {
            this.SaveChanges();
        }

        public DbSet<Domain.Token> Tokens { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
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
        }
    }
}