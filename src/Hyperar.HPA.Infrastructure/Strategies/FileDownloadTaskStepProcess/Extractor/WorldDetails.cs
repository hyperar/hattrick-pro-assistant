namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.Hattrick.WorldDetails;
    using Shared.Models.UI.Download;

    public class WorldDetails : FileDownloadTaskStepProcessStrategyBase, IFileDownloadTaskStepProcessStrategy
    {
        private const string LeagueFlagImageUrlMask = "/Img/flags/{0}.png";

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            IList<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
                    {
                        ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.XmlFile, nameof(xmlFileDownloadTask.XmlFile));

                        if (xmlFileDownloadTask.XmlFile is HattrickData file)
                        {
                            foreach (League league in file.LeagueList)
                            {
                                string imageUrl = string.Format(
                                    LeagueFlagImageUrlMask,
                                    league.LeagueId);

                                if (!ImageFileExists(imageUrl))
                                {
                                    fileDownloadTasks.Add(
                                        new ImageFileDownloadTask(
                                            imageUrl));
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
                }, cancellationToken);
            }
            catch
            {
                fileDownloadTask.Status = DownloadTaskStatus.Error;

                throw;
            }
        }
    }
}