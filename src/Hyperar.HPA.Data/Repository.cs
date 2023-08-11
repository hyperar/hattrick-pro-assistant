//-----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data
{
    using System;
    using System.Linq;
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// IEntity class Repository implementation.
    /// </summary>
    /// <typeparam name="TEntity">IEntity class.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, Domain.Interfaces.IEntity
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly IDatabaseContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">App Database Context.</param>
        /// <param name="queryStrategy">Query strategy.</param>
        public Repository(IDatabaseContext context)
        {
            this.context = context;
            this.EntityCollection = this.context.CreateSet<TEntity>();
        }

        /// <summary>
        /// Gets the entity collection.
        /// </summary>
        protected DbSet<TEntity> EntityCollection { get; private set; }

        /// <summary>
        /// Checks whether there is an object with the specified ID or not.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        /// <returns>A value indicating whether there is an object with the specified ID or not.</returns>
        public bool CheckIfExistsById(int id)
        {
            return this.EntityCollection.Any(e => e.Id == id);
        }

        /// <summary>
        /// Deletes the object with the specified ID.
        /// </summary>
        /// <param name="id">ID of the object to delete.</param>
        public void Delete(int id)
        {
            var entity = this.GetById(id) ?? throw new ArgumentNullException(nameof(id));

            this.context.CreateSet<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Gets the object with the specified ID, if any.
        /// </summary>
        /// <param name="id">ID of the desired object.</param>
        /// <returns>Entity with the specified ID, if any.</returns>
        public TEntity? GetById(int id)
        {
            return this.EntityCollection
                       .Where(e => e.Id == id)
                       .SingleOrDefault();
        }

        /// <summary>
        /// Inserts the specified entity on the database.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        public void Insert(TEntity entity)
        {
            this.EntityCollection.Add(entity);
        }

        /// <summary>
        /// Gets an IQueryable object with the entities that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Query filters.</param>
        /// <returns>IQueryable object with the entities that satisfy the specified predicate.</returns>
        public IQueryable<TEntity> Query(Func<TEntity, bool>? predicate = null)
        {
            var query = this.EntityCollection.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate)
                             .AsQueryable();
            }

            return query;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        public void Update(TEntity entity)
        {
            this.EntityCollection.Update(entity);
        }
    }
}