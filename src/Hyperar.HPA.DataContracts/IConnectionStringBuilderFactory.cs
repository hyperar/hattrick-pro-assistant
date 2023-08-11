//-----------------------------------------------------------------------
// <copyright file="IConnectionStringBuilderFactory.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    /// <summary>
    /// Connection String Builder Factory contract.
    /// </summary>
    public interface IConnectionStringBuilderFactory
    {
        /// <summary>
        /// Gets the Connection String Builder.
        /// </summary>
        /// <returns>The correct Connection String Builder.</returns>
        IConnectionStringBuilderStrategy GetConnectionStringBuilder();
    }
}