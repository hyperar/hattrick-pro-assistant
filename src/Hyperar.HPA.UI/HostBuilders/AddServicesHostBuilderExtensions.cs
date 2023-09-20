namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Infrastructure;
    using Hyperar.HPA.Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IHattrickService, HattrickService>();
                services.AddSingleton<IHomeViewService, HomeViewService>();
                services.AddSingleton<IProtectedResourceUrlBuilder, ProtectedResourceUrlBuilder>();
                services.AddScoped<ITokenService, TokenService>();
                services.AddSingleton<IXmlEntityFactory, XmlEntityFactory>();
                services.AddScoped<IXmlFileService, XmlFileService>();
            });

            return host;
        }
    }
}