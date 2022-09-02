//-----------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// IEntity base implementation.
    /// </summary>
    public abstract class EntityBase : IEntity, ICloneable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Creates a shallow copy of the current object.
        /// </summary>
        /// <returns>A shallow copy of the current object.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}