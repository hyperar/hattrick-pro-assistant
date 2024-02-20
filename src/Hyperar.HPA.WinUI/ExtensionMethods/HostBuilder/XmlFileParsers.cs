namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Application.Interfaces;
    using Infrastructure;
    using Infrastructure.Strategies.XmlFileParser;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class XmlFileParsers
    {
        public static IHostBuilder RegisterXmlFileParsers(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IXmlFileParserFactory, XmlFileParserFactory>();
                services.AddSingleton<ArenaDetails>();
                services.AddSingleton<Avatars>();
                services.AddSingleton<HallOfFamePlayers>();
                services.AddSingleton<ManagerCompendium>();
                services.AddSingleton<MatchDetails>();
                services.AddSingleton<Matches>();
                services.AddSingleton<MatchLineUp>();
                services.AddSingleton<Players>();
                services.AddSingleton<StaffAvatars>();
                services.AddSingleton<StaffList>();
                services.AddSingleton<TeamDetails>();
                services.AddSingleton<WorldDetails>();
            });

            return host;
        }
    }
}