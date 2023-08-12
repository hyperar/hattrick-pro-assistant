namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<Data.Strategies.ConnectionStringBuilder.AttachDatabaseFile>();
                services.AddSingleton<Data.Strategies.ConnectionStringBuilder.UseDatabase>();
                services.AddSingleton<DataContracts.IConnectionStringBuilderFactory, Data.Factories.ConnectionStringBuilder>();
                services.AddDbContext<DataContracts.IDatabaseContext, Data.DatabaseContext>();
                services.AddScoped(typeof(DataContracts.IRepository<>), typeof(Data.Repository<>));
                services.AddScoped(typeof(DataContracts.IHattrickRepository<>), typeof(Data.HattrickRepository<>));
            });

            return host;
        }
    }
}
