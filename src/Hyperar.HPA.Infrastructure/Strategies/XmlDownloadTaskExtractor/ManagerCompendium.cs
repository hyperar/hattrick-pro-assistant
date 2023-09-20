namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.ManagerCompendium;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.Common.Enums;

    public class ManagerCompendium : IXmlDownloadTaskExtractorStrategy
    {
        private const string includeRegionsParamKey = "includeRegions";

        private const string leagueIdParamKey = "leagueId";

        private const string userIdParamKey = "userId";

        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            HattrickData file = (HattrickData)xmlFile;

            if (file != null)
            {
                var downloadTasks = new List<DownloadTask>();

                foreach (Team curTeam in file.Manager.Teams)
                {
                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.WorldDetails,
                            new Dictionary<string, string>
                            {
                            { leagueIdParamKey, curTeam.League.LeagueId.ToString() },
                            { includeRegionsParamKey, bool.TrueString }
                            }));
                }

                downloadTasks.Add(
                    new DownloadTask(
                        XmlFileType.TeamDetails,
                        new Dictionary<string, string>
                        {
                        { userIdParamKey, file.Manager.UserId.ToString() }
                        }));

                return downloadTasks;
            }

            throw new ArgumentException($"Specified file is of the incorrect type: '{xmlFile.GetType()}'.");
        }
    }
}