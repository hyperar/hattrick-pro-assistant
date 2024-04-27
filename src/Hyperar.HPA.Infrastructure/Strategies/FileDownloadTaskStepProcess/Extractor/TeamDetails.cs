namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.Hattrick.TeamDetails;
    using Shared.Models.UI.Download;

    public class TeamDetails : FileDownloadTaskStepProcessStrategyBase, IFileDownloadTaskStepProcessStrategy
    {
        private const string arenaIdParamKey = "arenaId";

        private const string teamIdParamKey = "teamId";

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            ICollection<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(0, cancellationToken);

                if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
                {
                    ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.XmlFile, nameof(xmlFileDownloadTask.XmlFile));

                    if (xmlFileDownloadTask.XmlFile is HattrickData file)
                    {
                        foreach (Team curTeam in file.Teams)
                        {
                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.ArenaDetails,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { arenaIdParamKey, curTeam.Arena.Id.ToString() }
                                    }));

                            if (!string.IsNullOrWhiteSpace(curTeam.LogoUrl))
                            {
                                if (!ImageFileExists(curTeam.LogoUrl))
                                {
                                    fileDownloadTasks.Add(
                                        new ImageFileDownloadTask(
                                            curTeam.LogoUrl));
                                }
                            }

                            if (!ImageFileExists(curTeam.DressUri))
                            {
                                fileDownloadTasks.Add(
                                    new ImageFileDownloadTask(
                                        curTeam.DressUri));
                            }

                            if (!ImageFileExists(curTeam.DressAlternateUri))
                            {
                                fileDownloadTasks.Add(
                                    new ImageFileDownloadTask(
                                        curTeam.DressAlternateUri));
                            }

                            if (downloadSettings.DownloadFullMatchArchive)
                            {
                                throw new NotImplementedException(nameof(downloadSettings.DownloadFullMatchArchive));
                            }
                            else
                            {
                                fileDownloadTasks.Add(
                                    new XmlFileDownloadTask(
                                        XmlFileType.Matches,
                                        curTeam.TeamId,
                                        new NameValueCollection
                                        {
                                            { teamIdParamKey, curTeam.TeamId.ToString() }
                                        }));
                            }

                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.Players,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { teamIdParamKey, curTeam.TeamId.ToString() }
                                    }));

                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.Avatars,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { teamIdParamKey, curTeam.TeamId.ToString() }
                                    }));

                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.HallOfFamePlayers,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { teamIdParamKey, curTeam.TeamId.ToString() },
                                    }));

                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.StaffList,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { teamIdParamKey, curTeam.TeamId.ToString() },
                                    }));

                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.StaffAvatars,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { teamIdParamKey, curTeam.TeamId.ToString() },
                                    }));
                        }
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                Globalization.Translations.UnexpectedFileType,
                                typeof(HattrickData).FullName,
                                xmlFileDownloadTask.XmlFile.GetType().FullName));
                    }
                }
                else
                {
                    throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
                }
            }
            catch
            {
                fileDownloadTask.Status = DownloadTaskStatus.Error;

                throw;
            }
        }
    }
}