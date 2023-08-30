namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {

                string? connectionString = context.Configuration.GetConnectionString("LocalDb");
                Action<DbContextOptionsBuilder> configureDbContext = o =>
                {
                    o.UseLazyLoadingProxies();
                    o.UseSqlServer(connectionString);
                };

                services.AddSingleton(new Data.DatabaseContextFactory(configureDbContext));
                services.AddDbContext<Data.DatabaseContext>(configureDbContext);
            });

            return host;
        }
    }
}
