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

    public class ManagerCompendium : FileDownloadTaskStepProcessStrategyBase, IFileDownloadTaskStepProcessStrategy
    {
        private const string IncludeRegionsParamKey = "includeRegions";

        private const string LeagueIdParamKey = "leagueId";

        private const string UserIdParamKey = "userId";

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
                                        { LeagueIdParamKey, curTeam.League.LeagueId.ToString() },
                                        { IncludeRegionsParamKey, bool.TrueString }
                                    }));
                        }

                        fileDownloadTasks.Add(
                            new XmlFileDownloadTask(
                                XmlFileType.TeamDetails,
                                file.Manager.UserId,
                                new NameValueCollection
                                {
                                    { UserIdParamKey, file.Manager.UserId.ToString() }
                                }));

                        if (file.Manager.Avatar != null)
                        {
                            if (!ImageFileExists(file.Manager.Avatar.BackgroundImage))
                            {
                                fileDownloadTasks.Add(
                                    new ImageFileDownloadTask(
                                        file.Manager.Avatar.BackgroundImage));
                            }

                            foreach (Layer layer in file.Manager.Avatar.Layers)
                            {
                                if (!ImageFileExists(layer.Image))
                                {
                                    fileDownloadTasks.Add(
                                        new ImageFileDownloadTask(
                                            layer.Image));
                                }
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