namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Application.Interfaces;
    using Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class FileDownloadStepProcesses
    {
        public static IHostBuilder RegisterFileDownloadStepProcesses(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                // Factories.
                services.AddScoped<IXmlEntityFactory, XmlEntityFactory>();
                services.AddScoped<IFileDownloadTaskStepProcessAbstractFactory, FileDownloadTaskStepProcessAbstractFactory>();
                services.AddScoped<IFileDownloadTaskStepAdvancerFactory, FileDownloadTaskStepAdvancerFactory>();
                services.AddScoped<IImageFileDownloadTaskStepProcessFactory, ImageFileDownloadTaskStepProcessFactory>();
                services.AddScoped<IXmlFileDownloadTaskStepProcessFactory, XmlFileDownloadTaskStepProcessFactory>();
                services.AddScoped<IXmlFileDownloadTaskExtractStepProcessFactory, XmlFileDownloadTaskExtractStepProcessFactory>();
                services.AddScoped<IXmlFileDownloadTaskParseStepProcessFactory, XmlFileDownloadTaskParseStepProcessFactory>();
                services.AddScoped<IXmlFileDownloadTaskPersistStepProcessFactory, XmlFileDownloadTaskPersistStepProcessFactory>();

                // Advancers.
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Advancer.ImageFile>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Advancer.XmlFile>();

                // Downloaders.
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Downloader.ImageFile>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Downloader.XmlFile>();

                // Extractors.
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.ArenaDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.Avatars>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.CheckToken>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.Default>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.ManagerCompendium>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.MatchArchive>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.Matches>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.Players>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.StaffAvatars>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.TeamDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor.WorldDetails>();

                // Parsers.
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.ArenaDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.Avatars>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.CheckToken>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.HallOfFamePlayers>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.ManagerCompendium>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.MatchArchive>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.MatchDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.Matches>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.MatchLineUp>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.Players>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.PlayerDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.StaffAvatars>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.StaffList>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.TeamDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.WorldDetails>();

                // Persisters.
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.Avatars>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.ArenaDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.Default>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.HallOfFamePlayers>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.ManagerCompendium>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.MatchDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.MatchLineUp>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.Players>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.PlayerDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.StaffAvatars>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.StaffList>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.TeamDetails>();
                services.AddScoped<Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister.WorldDetails>();
            });

            return host;
        }
    }
}