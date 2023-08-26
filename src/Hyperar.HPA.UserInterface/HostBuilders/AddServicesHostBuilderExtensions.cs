namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using Hyperar.HPA.Business;
    using Hyperar.HPA.BusinessContracts;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IHattrickService, HattrickService>();
                services.AddSingleton<IProtectedResourceUrlBuilder, ProtectedResourceUrlBuilder>();
                services.AddSingleton<ITokenService, TokenService>();
                services.AddSingleton<IXmlChildDownloadTaskBuilderFactory, XmlChildDownloadTaskBuilderFactory>();
                services.AddSingleton<IXmlEntityFactory, XmlEntityFactory>();
                services.AddSingleton<IXmlFileParserFactory, XmlFileParserFactory>();
                services.AddSingleton<IXmlFileService, XmlFileService>();
            });

            return host;
        }
    }
}
