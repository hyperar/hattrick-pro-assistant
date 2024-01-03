namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.TeamDetails;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.Common.Enums;

    public class TeamDetails : IXmlDownloadTaskExtractorStrategy
    {
        private const string arenaIdParamKey = "arenaId";

        private const string teamIdParamKey = "teamId";

        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            HattrickData file = (HattrickData)xmlFile;

            if (file != null)
            {
                var downloadTasks = new List<DownloadTask>();

                foreach (Team curTeam in file.Teams)
                {
                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.ArenaDetails,
                            new Dictionary<string, string>
                            {
                                { arenaIdParamKey, curTeam.Arena.ArenaId.ToString() }
                            }));

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.Players,
                            new Dictionary<string, string>
                            {
                                { teamIdParamKey, curTeam.TeamId.ToString() }
                            }));
                }

                return downloadTasks;
            }

            throw new ArgumentException($"Specified file is of the incorrect type: '{xmlFile.GetType()}'.");
        }
    }
}