namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class FileDownload
    {
        public static IHostBuilder RegisterFileDownloadStepProcesses(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddScoped<Application.Interfaces.IDownloadRequestFactory, Infrastructure.DownloadRequestFactory>();

                services.AddScoped<Infrastructure.Features.Download.Download.Strategies.ImageFile>();
                services.AddScoped<Infrastructure.Features.Download.Download.Strategies.XmlFile>();

                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.Avatars>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.CheckToken>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.Default>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.ManagerCompendium>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.MatchArchive>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.MatchDetails>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.Matches>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.Players>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.TeamDetails>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.WorldDetails>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.YouthAvatars>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.YouthPlayerList>();
                services.AddScoped<Infrastructure.Features.Download.Extract.Strategies.YouthTeamDetails>();

                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.Avatars>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.CheckToken>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.Default>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.LeagueDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.ManagerCompendium>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.MatchArchive>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.MatchDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.Matches>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.MatchLineUp>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.PlayerDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.Players>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.StaffAvatars>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.StaffList>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.TeamDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.WorldDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.YouthAvatars>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.YouthLeagueDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.YouthPlayerDetails>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.YouthPlayerList>();
                services.AddScoped<Infrastructure.Features.Download.Parse.Strategies.YouthTeamDetails>();

                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.Avatars>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.Default>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.LeagueDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.ManagerCompendium>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.MatchDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.Matches>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.MatchLineUp>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.PlayerDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.Players>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.TeamDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.WorldDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.YouthAvatars>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.YouthLeagueDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.YouthPlayerDetails>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.YouthPlayerList>();
                services.AddScoped<Infrastructure.Features.Download.Persist.Strategies.YouthTeamDetails>();

                services.AddScoped<Application.Interfaces.IXmlEntityFactory, Infrastructure.XmlEntityFactory>();
                services.AddScoped<Application.Interfaces.IDownloaderFactory, Infrastructure.Features.Download.Download.DownloaderFactory>();
                services.AddScoped<Application.Interfaces.IExtractorFactory, Infrastructure.Features.Download.Extract.ExtractorFactory>();
                services.AddScoped<Application.Interfaces.IParserFactory, Infrastructure.Features.Download.Parse.ParserFactory>();
                services.AddScoped<Application.Interfaces.IPersisterFactory, Infrastructure.Features.Download.Persist.PersisterFactory>();

                services.AddMediatR((config) =>
                {
                    config.Lifetime = ServiceLifetime.Scoped;
                    config.RegisterServicesFromAssembly(Assembly.Load("Hyperar.HPA.Infrastructure"));
                });
            });

            return host;
        }
    }
}