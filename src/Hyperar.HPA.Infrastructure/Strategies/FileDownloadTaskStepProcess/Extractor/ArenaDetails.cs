namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.Hattrick.ArenaDetails;
    using Shared.Models.UI.Download;

    public class ArenaDetails : IFileDownloadTaskStepProcessStrategy
    {
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
                        fileDownloadTasks.Add(
                            new ImageFileDownloadTask(
                                string.IsNullOrWhiteSpace(file.Arena.ArenaImage) ? file.Arena.ArenaFallbackImage : file.Arena.ArenaImage));
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