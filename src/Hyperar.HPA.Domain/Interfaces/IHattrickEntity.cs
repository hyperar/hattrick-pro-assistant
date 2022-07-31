//-----------------------------------------------------------------------
// <copyright file="IHattrickEntity.cs" company="Hyperar">
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
