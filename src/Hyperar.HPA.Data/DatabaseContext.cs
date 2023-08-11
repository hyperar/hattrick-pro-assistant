//-----------------------------------------------------------------------
// <copyright file="DatabaseContext.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data
{
    using System;
    using Hyperar.HPA.DataContracts;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// Connection String Builder factory.
        /// </summary>
        private readonly IConnectionStringBuilderFactory? connectionStringBuilderFactory;

        /// <summary>
        /// Indicates whether the execution context is running/creating Entity Framework Core migrations.
        /// </summary>
        private readonly bool isMigrationsContext;

        /// <summary>
        /// Indicates whether the current transaction is cancelled or not.
        /// </summary>
        private bool cancelled;

        /// <summary>
        /// Initializes a new instance of the see <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <remarks>
        /// This constructor only gets executed when creating migrations, that's the only reason it's here.
        /// </remarks>
        public DatabaseContext()
        {
            this.connectionStringBuilderFactory = null;
            this.isMigrationsContext = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <param name="options">The Database Context options.</param>
        /// <param name="connectionStringBuilderFactory">The Connection String Builder Factory.</param>
        public DatabaseContext(DbContextOptions options, IConnectionStringBuilderFactory connectionStringBuilderFactory) : base(options)
        {
            this.isMigrationsContext = false;

            this.connectionStringBuilderFactory = connectionStringBuilderFactory;

            this.Database.Migrate();
        }

        /// <summary>
        /// Initializes a new transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (this.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException();
            }

            this.Database.BeginTransaction();
        }

        /// <summary>
        /// Sets the current transaction as cancelled so when the EndTransaction method is called
        /// changes undone.
        /// </summary>
        public void Cancel()
        {
            this.cancelled = true;
        }

        /// <summary>
        /// Returns a IQueryable instance for access to entities of the given type in the context and
        /// the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class, IEntity
        {
            return this.Set<TEntity>();
        }

        /// <summary>
        /// Commits or rollbacks the pending changes depending on the transaction state.
        /// </summary>
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

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <remarks>In case there's an active transaction changes will be saved within its scope.</remarks>
        public void Save()
        {
            this.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <exception cref="FieldAccessException">In the case where the execution flow gets to a null reference ConnectionStringBuilderFactory. This shouldn't happen at runtime.</exception>
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

        /// <summary>
        /// Model definition.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        /// <summary>
        /// Save changes and commits the transaction, if any.
        /// </summary>
        private void Commit()
        {
            this.SaveChanges();

            this.Database.CurrentTransaction?.Commit();
        }

        /// <summary>
        /// Revert changes and rollbacks the transaction, if any.
        /// </summary>
        private void Rollback()
        {
            this.ChangeTracker.Clear();

            this.Database.CurrentTransaction?.Rollback();

            this.cancelled = false;
        }
    }
}