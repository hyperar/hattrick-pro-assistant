namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddXmlFileParsersHostBuilderExtensions
    {
        public static IHostBuilder AddParsers(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IXmlFileParserFactory, XmlFileParserFactory>();
                services.AddSingleton<ArenaDetails>();
                services.AddSingleton<ManagerCompendium>();
                services.AddSingleton<Players>();
                services.AddSingleton<TeamDetails>();
                services.AddSingleton<WorldDetails>();
            });

            return host;
        }
    }
}