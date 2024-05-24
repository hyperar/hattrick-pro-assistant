namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Application.Interfaces;
    using Application.Services;
    using Infrastructure;
    using Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class Services
    {
        public static IHostBuilder RegisterServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<IHattrickService, HattrickService>();
                services.AddScoped<IProtectedResourceUrlBuilder, ProtectedResourceUrlBuilder>();
                services.AddSingleton<IXmlEntityFactory, XmlEntityFactory>();

                // View services.
                services.AddScoped<IDownloadViewService, DownloadViewService>();
                services.AddScoped<IHomeViewService, HomeViewService>();
                services.AddScoped<IPlayersViewService, PlayersViewService>();
                services.AddScoped<ITeamSelectionViewService, TeamSelectionViewService>();

                // Scoped services.
                //services.AddScoped<IDownloadService, DownloadService>();
                services.AddScoped<IUserService, UserService>();
            });

            return host;
        }
    }
}