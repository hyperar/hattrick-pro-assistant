//-----------------------------------------------------------------------
// <copyright file="Country.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    public class Country : HattrickEntityBase, IEntity, IHattrickEntity
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}