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
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.ManagerCompendium;

    using Shared.Models.UI.Download;

    public class ManagerCompendium : IFileDownloadTaskStepProcessStrategy
    {
        private const string includeRegionsParamKey = "includeRegions";

        private const string leagueIdParamKey = "leagueId";

        private const string userIdParamKey = "userId";

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
                        foreach (Team curTeam in file.Manager.Teams)
                        {
                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.WorldDetails,
                                    curTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { leagueIdParamKey, curTeam.League.LeagueId.ToString() },
                                        { includeRegionsParamKey, bool.TrueString }
                                    }));
                        }

                        fileDownloadTasks.Add(
                            new XmlFileDownloadTask(
                                XmlFileType.TeamDetails,
                                file.Manager.UserId,
                                new NameValueCollection
                                {
                                    { userIdParamKey, file.Manager.UserId.ToString() }
                                }));

                        if (file.Manager.Avatar != null)
                        {
                            fileDownloadTasks.Add(
                                new ImageFileDownloadTask(
                                    file.Manager.Avatar.BackgroundImage));

                            foreach (Layer layer in file.Manager.Avatar.Layers)
                            {
                                fileDownloadTasks.Add(
                                    new ImageFileDownloadTask(
                                        layer.Image));
                            }
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