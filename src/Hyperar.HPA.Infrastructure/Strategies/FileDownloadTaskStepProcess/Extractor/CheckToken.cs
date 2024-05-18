namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.Hattrick.CheckToken;
    using Shared.Models.UI.Download;

    public class CheckToken : IFileDownloadTaskStepProcessStrategy
    {
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
                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.WorldDetails));

                            fileDownloadTasks.Add(
                                new XmlFileDownloadTask(
                                    XmlFileType.ManagerCompendium));
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