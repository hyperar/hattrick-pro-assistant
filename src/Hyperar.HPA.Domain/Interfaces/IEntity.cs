//-----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Entity contract.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        int Id { get; set; }
    }
}
