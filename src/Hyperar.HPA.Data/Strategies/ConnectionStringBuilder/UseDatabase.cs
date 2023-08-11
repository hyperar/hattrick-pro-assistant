//-----------------------------------------------------------------------
// <copyright file="UseDatabase.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data.Strategies.ConnectionStringBuilder
{
    using Hyperar.HPA.DataContracts;

    /// <summary>
    /// Builds a connection string with for existing database.
    /// </summary>
    public class UseDatabase : IConnectionStringBuilderStrategy
    {
        /// <summary>
        /// Gets the connection string with for existing database.
        /// </summary>
        /// <returns>A connection string for creating the database.</returns>
        public string GetConnectionString()
        {
            return $"Data Source=(localdb)\\HPA;Initial Catalog=HPADB;Integrated Security=True;";
        }
    }
}