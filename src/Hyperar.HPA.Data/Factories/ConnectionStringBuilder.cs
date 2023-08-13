namespace Hyperar.HPA.Data.Factories
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using Hyperar.HPA.DataContracts;
    using Hyperar.HPA.Data.Strategies.ConnectionStringBuilder;
    using Microsoft.Data.SqlClient;

    public class ConnectionStringBuilder : IConnectionStringBuilderFactory
    {
        private const string CheckDatabaseCommandText = "SELECT [name] FROM [sys].[databases] WHERE [name] IN ('HPADB', '[HPADB]')";

        private const string DefaultConnectionString = "Data Source=(localdb)\\HPA;Initial Catalog=master;Integrated Security=True;";

        private readonly AttachDatabaseFile attachDatabaseFileStrategy;

        private readonly UseDatabase useDatabaseStrategy;

        public ConnectionStringBuilder(
            AttachDatabaseFile attachDatabaseFileStrategy,
            UseDatabase useDatabaseStrategy)
        {
            this.attachDatabaseFileStrategy = attachDatabaseFileStrategy;
            this.useDatabaseStrategy = useDatabaseStrategy;
        }

        public IConnectionStringBuilderStrategy GetConnectionStringBuilder()
        {
            EnsureDatabaseInstanceIsReady();

            return CheckIfDatabaseExists() ? this.useDatabaseStrategy : this.attachDatabaseFileStrategy;
        }

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