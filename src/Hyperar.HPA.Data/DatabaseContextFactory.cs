namespace Hyperar.HPA.Data
{
    using System;
    using System.Diagnostics;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContextFactory
    {
        private const string sqlCommandName = "sqllocaldb.exe";

        private const string sqlCommandParameters = "c HPA -s";

        private readonly Action<DbContextOptionsBuilder> configureDbContext;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            this.configureDbContext = configureDbContext;
        }

        public DatabaseContext CreateDbContext()
        {
            EnsureDatabaseInstanceIsReady();

            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();

            this.configureDbContext(options);

            return new DatabaseContext(options.Options);
        }

        private static void EnsureDatabaseInstanceIsReady()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = sqlCommandParameters, // Creates and starts the instance.
                    CreateNoWindow = true,
                    FileName = sqlCommandName,
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
