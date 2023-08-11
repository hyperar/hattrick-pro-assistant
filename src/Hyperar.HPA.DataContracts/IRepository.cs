//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// Entity repository contract.
    /// </summary>
    /// <typeparam name="TEntity">IEntity class.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Checks whether there is an object with the specified ID or not.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        /// <returns>A value indicating whether there is an object with the specified ID or not.</returns>
        bool CheckIfExistsById(int id);

        /// <summary>
        /// Deletes the object with the specified ID.
        /// </summary>
        /// <param name="id">ID of the object to delete.</param>
        void Delete(int id);

        /// <summary>
        /// Gets the object with the specified ID, if any.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        /// <returns>Entity with the specified ID, if any.</returns>
        TEntity? GetById(int id);

        /// <summary>
        /// Inserts the specified entity on the database.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Gets an IQueryable object with the entities that satisfy the specified predicate, if any.
        /// </summary>
        /// <param name="predicate">Query filters.</param>
        /// <returns>IQueryable object with the entities that satisfy the specified predicate.</returns>
        IQueryable<TEntity> Query(Func<TEntity, bool>? predicate = null);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(TEntity entity);
    }
}