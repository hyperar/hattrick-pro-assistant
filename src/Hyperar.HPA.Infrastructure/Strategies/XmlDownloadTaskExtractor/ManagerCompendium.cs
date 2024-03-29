﻿namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.ManagerCompendium;
    using Application.Interfaces;
    using Application.Models;
    using Common.Enums;

    public class ManagerCompendium : IXmlDownloadTaskExtractorStrategy
    {
        private const string includeRegionsParamKey = "includeRegions";

        private const string leagueIdParamKey = "leagueId";

        private const string userIdParamKey = "userId";

        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            if (xmlFile is HattrickData file)
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
            else
            {
                throw new ArgumentException(xmlFile.GetType().FullName, nameof(xmlFile));
            }
        }
    }
}