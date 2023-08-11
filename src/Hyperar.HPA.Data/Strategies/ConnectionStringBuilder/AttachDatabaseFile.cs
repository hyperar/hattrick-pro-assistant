//-----------------------------------------------------------------------
// <copyright file="AttachDatabaseFile.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data.Strategies.ConnectionStringBuilder
{
    using System;
    using Hyperar.HPA.DataContracts;

    /// <summary>
    /// Builds a connection string with file attach for database creation.
    /// </summary>
    public class AttachDatabaseFile : IConnectionStringBuilderStrategy
    {
        /// <summary>
        /// Gets the connection string with file attach for database creation.
        /// </summary>
        /// <returns>A connection string for creating the database.</returns>
        public string GetConnectionString()
        {
            return $"Data Source=(localdb)\\HPA;AttachDbFilename={this.GetDatabaseFolder()}\\HPADB.mdf;Initial Catalog=HPADB;Integrated Security=True;";
        }

        /// <summary>
        /// Get the database folder.
        /// </summary>
        /// <returns>Full path to database.</returns>
        private string GetDatabaseFolder()
        {
            string databaseFolder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                Common.Constants.Folders.AppName,
                Common.Constants.Folders.Database);

            if (!Directory.Exists(databaseFolder))
            {
                Directory.CreateDirectory(databaseFolder);
            }

            return databaseFolder;
        }
    }
}