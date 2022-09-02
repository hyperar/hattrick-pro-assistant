//-----------------------------------------------------------------------
// <copyright file="Manager.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// Team manager class.
    /// </summary>
    public class Manager : HattrickEntityBase, IEntity, IHattrickEntity
    {
        /// <summary>
        /// Gets or sets the Supporter Tier.
        /// </summary>
        public SupporterTier SupporterTier { get; set; }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}