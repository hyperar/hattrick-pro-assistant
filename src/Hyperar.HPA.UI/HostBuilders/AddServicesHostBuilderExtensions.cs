namespace Hyperar.HPA.UI.HostBuilders
{
    using Application.Interfaces;
    using Application.Services;
    using Infrastructure;
    using Infrastructure.Services;
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
                services.AddScoped<IHomeViewService, HomeViewService>();
                services.AddScoped<IPlayersViewService, PlayersViewService>();
                services.AddScoped<IUserService, UserService>();
                services.AddSingleton<IXmlEntityFactory, XmlEntityFactory>();
                services.AddScoped<IXmlFileService, XmlFileService>();
            });

            return host;
        }
    }
}