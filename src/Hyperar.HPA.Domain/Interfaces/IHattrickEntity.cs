//-----------------------------------------------------------------------
// <copyright file="IHattrickEntity.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain.Interfaces
{
    /// <summary>
    /// Hattrick entity contract.
    /// </summary>
    public interface IHattrickEntity
    {
        /// <summary>
        /// Gets or sets the Hattrick Id.
        /// </summary>
        uint HattrickId { get; set; }
    }
}