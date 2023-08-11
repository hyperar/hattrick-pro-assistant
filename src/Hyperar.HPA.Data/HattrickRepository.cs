//-----------------------------------------------------------------------
// <copyright file="HattrickRepository.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data
{
    using System.Linq;
    using Hyperar.HPA.DataContracts;

    /// <summary>
    /// IHattrickEntity class Repository implementation.
    /// </summary>
    /// <typeparam name="TEntity">IHattrickEntity class.</typeparam>
    public class HattrickRepository<TEntity> : Repository<TEntity>, IHattrickRepository<TEntity> where TEntity : class, Domain.Interfaces.IEntity, Domain.Interfaces.IHattrickEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HattrickRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">App Database Context.</param>
        /// <param name="queryStrategy">Query Strategy.</param>
        public HattrickRepository(IDatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Checks whether there is an object with the specified ID or not.
        /// </summary>
        /// <param name="hattrickId">ID of the object.</param>
        /// <returns>A value indicating whether there is an object with the specified ID or not.</returns>
        public bool CheckIfExistsByHattrickId(uint hattrickId)
        {
            return this.EntityCollection.Any(e => e.HattrickId == hattrickId);
        }

        /// <summary>
        /// Gets the object with the specified Hattrick ID, if any.
        /// </summary>
        /// <param name="hattrickId">Hattrick ID of the desired object.</param>
        /// <returns>Entity with the specified Hattrick ID, if any.</returns>
        public TEntity? GetByHattrickId(long hattrickId)
        {
            return this.EntityCollection.SingleOrDefault(e => e.HattrickId == hattrickId);
        }
    }
}