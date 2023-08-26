namespace Hyperar.HPA.Business.XmlChildDownloadTaskBuilder
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Hattrick;
    using Hyperar.HPA.Domain.Hattrick.ManagerCompendium;

    public class ManagerCompendium : IXmlChildDownloadTaskBuilderStrategy
    {
        private const string leagueIdParamKey = "leagueId";

        private const string includeRegionsParamKey = "includeRegions";

        private const string userIdParamKey = "userId";

        public List<DownloadTask>? BuildChildDownloadTaskList(XmlFileBase xmlFile)
        {
            HattrickData file = (HattrickData)xmlFile;

            if (file == null)
            {
                throw new ArgumentException($"Specified file is of the incorrect type: '{xmlFile.GetType()}'.");
            }

            List<DownloadTask> downloadTasks = new List<DownloadTask>();

            foreach (var curTeam in file.Manager.Teams)
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
    }
}
