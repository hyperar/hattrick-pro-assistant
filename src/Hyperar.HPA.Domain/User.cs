//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    /// <summary>
    /// Application user class.
    /// </summary>
    public class User : EntityBase, IEntity
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the token was created.
        /// </summary>
        public DateTime TokenCreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the token expires.
        /// </summary>
        public DateTime TokenExpiresOn { get; set; }

        /// <summary>
        /// Gets or sets the token secret.
        /// </summary>
        public string TokenSecret { get; set; } = string.Empty;
    }
}