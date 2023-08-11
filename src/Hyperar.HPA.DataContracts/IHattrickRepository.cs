//-----------------------------------------------------------------------
// <copyright file="IHattrickRepository.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// Generic Hattrick entity repository contract.
    /// </summary>
    /// <typeparam name="TEntity">IHattrickEntity class.</typeparam>
    public interface IHattrickRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, IHattrickEntity
    {
        /// <summary>
        /// Checks whether there is an object with the specified ID or not.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        /// <returns>A value indicating whether there is an object with the specified ID or not.</returns>
        bool CheckIfExistsByHattrickId(uint hattrickId);

        /// <summary>
        /// Gets the object with the specified Hattrick ID, if any.
        /// </summary>
        /// <param name="hattrickId">Hattrick ID of the desired object.</param>
        /// <returns>Entity with the specified Hattrick ID, if any.</returns>
        TEntity? GetByHattrickId(long hattrickId);
    }
}