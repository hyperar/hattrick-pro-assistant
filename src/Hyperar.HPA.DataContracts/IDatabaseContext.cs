//-----------------------------------------------------------------------
// <copyright file="IDatabaseContext.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Application Database Context contract.
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Initializes a new transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Sets the current transaction as cancelled so when the EndTransaction method is called
        /// changes undone.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Returns a IQueryable instance for access to entities of the given type in the context and
        /// the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Commits or rollbacks the pending changes depending on the transaction state.
        /// </summary>
        void EndTransaction();

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <remarks>In case there's an active transaction changes will be saved within its scope.</remarks>
        void Save();
    }
}