//-----------------------------------------------------------------------
// <copyright file="HattrickEntityBase.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// IHattrickEntity base implementation.
    /// </summary>
    public class HattrickEntityBase : EntityBase, IEntity, IHattrickEntity
    {
        /// <summary>
        /// Gets or sets the Hattrick Id.
        /// </summary>
        public uint HattrickId { get; set; }
    }
}