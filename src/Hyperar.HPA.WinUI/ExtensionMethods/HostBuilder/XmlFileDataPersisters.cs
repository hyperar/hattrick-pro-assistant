namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Application.Interfaces;
    using Infrastructure;
    using Infrastructure.Strategies.XmlFileDataPersister;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class XmlFileDataPersisters
    {
        public static IHostBuilder RegisterXmlFileDataPersisters(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<IXmlFileDataPersisterFactory, XmlFileDataPersisterFactory>();
                services.AddScoped<ArenaDetails>();
                services.AddScoped<Avatars>();
                services.AddScoped<HallOfFamePlayers>();
                services.AddScoped<ManagerCompendium>();
                services.AddScoped<MatchDetails>();
                services.AddScoped<Matches>();
                services.AddScoped<MatchLineUp>();
                services.AddScoped<Players>();
                services.AddScoped<StaffAvatars>();
                services.AddScoped<StaffList>();
                services.AddScoped<TeamDetails>();
                services.AddScoped<WorldDetails>();
            });

            return host;
        }
    }
}