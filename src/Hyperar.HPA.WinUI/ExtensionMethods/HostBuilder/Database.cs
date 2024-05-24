namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Domain.Interfaces;
    using Infrastructure.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class Database
    {
        public static IHostBuilder RegisterDatabaseObjects(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string? connectionString = context.Configuration.GetConnectionString("LocalDb");

                void configureDbContext(DbContextOptionsBuilder o)
                {
                    o.UseLazyLoadingProxies();
                    o.UseSqlServer(connectionString);
                }

                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped(typeof(IHattrickRepository<>), typeof(HattrickRepository<>));
                services.AddDbContext<IDatabaseContext, DatabaseContext>(configureDbContext, ServiceLifetime.Scoped);
            });

            return host;
        }
    }
}