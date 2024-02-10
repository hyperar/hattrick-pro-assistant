namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Application.Interfaces;
    using Infrastructure;
    using Infrastructure.Strategies.XmlDownloadTaskExtractor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class XmlDownloadTaskExtractors
    {
        public static IHostBuilder RegisterXmlDownloadTaskExtractors(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IXmlDownloadTaskExtractorFactory, XmlDownloadTaskExtractorFactory>();
                services.AddSingleton<Default>();
                services.AddSingleton<ManagerCompendium>();
                services.AddSingleton<Matches>();
                services.AddSingleton<TeamDetails>();
            });

            return host;
        }
    }
}