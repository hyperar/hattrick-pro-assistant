namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddXmlFileDataPersistersHostBuilderExtensions
    {
        public static IHostBuilder AddPersisters(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<IXmlFileDataPersisterFactory, XmlFileDataPersisterFactory>();
                services.AddScoped<ArenaDetails>();
                services.AddScoped<ManagerCompendium>();
                services.AddScoped<Players>();
                services.AddScoped<TeamDetails>();
                services.AddScoped<WorldDetails>();
            });

            return host;
        }
    }
}