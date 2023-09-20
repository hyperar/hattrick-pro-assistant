namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure;
    using Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddXmlDownloadTaskExtractorsHostBuilderExtensions
    {
        public static IHostBuilder AddExtractors(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IXmlDownloadTaskExtractorFactory, XmlDownloadTaskExtractorFactory>();
                services.AddSingleton<Default>();
                services.AddSingleton<ManagerCompendium>();
                services.AddSingleton<TeamDetails>();
            });

            return host;
        }
    }
}