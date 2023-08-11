//-----------------------------------------------------------------------
// <copyright file="IQueryStrategy.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    using System.Linq;
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// Query strategy contract.
    /// </summary>
    /// <typeparam name="T">IEntity object.</typeparam>
    public interface IQueryStrategy<T> where T : class, IEntity
    {
        /// <summary>
        /// Applies the corresponding Includes depending on the class.
        /// </summary>
        /// <param name="query">Query to apply includes to.</param>
        /// <returns>Query with included child entities.</returns>
        IQueryable<T> ApplyIncludes(IQueryable<T> query);
    }
}