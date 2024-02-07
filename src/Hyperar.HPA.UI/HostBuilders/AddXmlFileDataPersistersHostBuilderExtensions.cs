namespace Hyperar.HPA.UI.HostBuilders
{
    using Application.Interfaces;
    using Infrastructure;
    using Infrastructure.Strategies.XmlFileDataPersister;
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
                services.AddScoped<Avatars>();
                services.AddScoped<HallOfFamePlayers>();
                services.AddScoped<ManagerCompendium>();
                services.AddScoped<MatchDetails>();
                services.AddScoped<Matches>();
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