namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.TeamDetails;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.Common.Enums;

    public class TeamDetails : IXmlDownloadTaskExtractorStrategy
    {
        private const string arenaIdParamKey = "arenaId";

        private const string teamIdParamKey = "teamId";

        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            if (xmlFile is HattrickData file)
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
                            XmlFileType.Matches,
                            new Dictionary<string, string>
                            {
                                { teamIdParamKey, curTeam.TeamId.ToString() }
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
            else
            {
                throw new ArgumentException(xmlFile.GetType().FullName, nameof(xmlFile));
            }
        }
    }
}