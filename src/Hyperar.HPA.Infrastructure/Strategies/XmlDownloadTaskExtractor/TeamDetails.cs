namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.TeamDetails;
    using Application.Interfaces;
    using Application.Models;
    using Common.Enums;

    public class TeamDetails : IXmlDownloadTaskExtractorStrategy
    {
        private const string arenaIdParamKey = "arenaId";

        private const string teamIdParamKey = "teamId";

        public DownloadTask[] ExtractXmlDownloadTasks(IXmlFile xmlFile)
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

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.Avatars,
                            new Dictionary<string, string>
                            {
                                { teamIdParamKey, curTeam.TeamId.ToString() }
                            }));

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.HallOfFamePlayers,
                            new Dictionary<string, string>
                            {
                                { teamIdParamKey, curTeam.TeamId.ToString() },
                            }));

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.StaffList,
                            new Dictionary<string, string>
                            {
                                { teamIdParamKey, curTeam.TeamId.ToString() },
                            }));

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.StaffAvatars,
                            new Dictionary<string, string>
                            {
                                { teamIdParamKey, curTeam.TeamId.ToString() },
                            }));
                }

                return downloadTasks.ToArray();
            }
            else
            {
                throw new ArgumentException(xmlFile.GetType().FullName, nameof(xmlFile));
            }
        }
    }
}