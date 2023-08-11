//-----------------------------------------------------------------------
// <copyright file="IConnectionStringBuilderStrategy.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    /// <summary>
    /// Connection String Builder Strategy contract.
    /// </summary>
    public interface IConnectionStringBuilderStrategy
    {
        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <returns>The database connection string.</returns>
        string GetConnectionString();
    }
}