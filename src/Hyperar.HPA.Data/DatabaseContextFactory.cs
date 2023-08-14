namespace Hyperar.HPA.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> configureDbContext;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            this.configureDbContext = configureDbContext;
        }

        public DatabaseContext CreateDbContext()
        {
            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();

            this.configureDbContext(options);

            return new DatabaseContext(options.Options);
        }
    }
}
