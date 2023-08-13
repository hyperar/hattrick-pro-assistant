namespace Hyperar.HPA.Data
{
    using System;
    using Hyperar.HPA.DataContracts;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly IConnectionStringBuilderFactory? connectionStringBuilderFactory;

        private readonly bool isMigrationsContext;

        private bool cancelled;

        public DatabaseContext()
        {
            this.connectionStringBuilderFactory = null;
            this.isMigrationsContext = true;
        }

        public DatabaseContext(DbContextOptions options, IConnectionStringBuilderFactory connectionStringBuilderFactory) : base(options)
        {
            this.isMigrationsContext = false;

            this.connectionStringBuilderFactory = connectionStringBuilderFactory;

            this.Database.Migrate();
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

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class, IEntity
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

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (this.isMigrationsContext)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\HPA;Initial Catalog=HPADB;Integrated Security=True;");
            }
            else
            {
                if (this.connectionStringBuilderFactory == null)
                {
                    throw new FieldAccessException(nameof(this.connectionStringBuilderFactory));
                }

                optionsBuilder.UseSqlServer(
                            this.connectionStringBuilderFactory.GetConnectionStringBuilder().GetConnectionString());
            }
        }

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