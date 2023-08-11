//-----------------------------------------------------------------------
// <copyright file="ConnectionStringBuilder.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data.Factories
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using Hyperar.HPA.DataContracts;
    using Hyperar.HPA.Data.Strategies.ConnectionStringBuilder;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Connection String Builder Factory implementation.
    /// </summary>
    public class ConnectionStringBuilder : IConnectionStringBuilderFactory
    {
        /// <summary>
        /// Check database query command text.
        /// </summary>
        private const string CheckDatabaseCommandText = "SELECT [name] FROM [sys].[databases] WHERE [name] IN ('HPADB', '[HPADB]')";

        /// <summary>
        /// Default database connection string.
        /// </summary>
        private const string DefaultConnectionString = "Data Source=(localdb)\\HPA;Initial Catalog=master;Integrated Security=True;";

        /// <summary>
        /// The AttachDatabaseFile connection string builder strategy.
        /// </summary>
        private readonly AttachDatabaseFile attachDatabaseFileStrategy;

        /// <summary>
        /// The UseDatabase connection string builder strategy.
        /// </summary>
        private readonly UseDatabase useDatabaseStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringBuilder" /> class.
        /// </summary>
        /// <param name="attachDatabaseFileStrategy">The AttachDatabaseFile connection string builder strategy.</param>
        /// <param name="useDatabaseStrategy">The UseDatabase connection string builder strategy.</param>
        public ConnectionStringBuilder(
            AttachDatabaseFile attachDatabaseFileStrategy,
            UseDatabase useDatabaseStrategy)
        {
            this.attachDatabaseFileStrategy = attachDatabaseFileStrategy;
            this.useDatabaseStrategy = useDatabaseStrategy;
        }

        /// <summary>
        /// Gets the correct database connection string strategy.
        /// </summary>
        /// <returns>A IConnectionStringBuilderStrategy with the correct implementation.</returns>
        public IConnectionStringBuilderStrategy GetConnectionStringBuilder()
        {
            EnsureDatabaseInstanceIsReady();

            return CheckIfDatabaseExists() ? this.useDatabaseStrategy : this.attachDatabaseFileStrategy;
        }

        /// <summary>
        /// Checks whether the database exists or not.
        /// </summary>
        /// <returns>A boolean value indicating whether the database exists or not.</returns>
        private static bool CheckIfDatabaseExists()
        {
            var connection = new SqlConnection(DefaultConnectionString);

            try
            {
                using (var command = new SqlCommand(CheckDatabaseCommandText, connection))
                {
                    connection.Open();

                    var result = command.ExecuteScalar();

                    return result != null && result != DBNull.Value;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Executes the SQL Local DB tool to create and start the database instance.
        /// </summary>
        /// <remarks>The tool has no effect if the database instance is already created and/or started, performing only the required tasks.</remarks>
        private static void EnsureDatabaseInstanceIsReady()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = "c HPA -s", // Creates and starts the instance.
                    CreateNoWindow = true,
                    FileName = "sqllocaldb.exe",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            };

            process.Start();

            process.WaitForExit();
        }
    }
}