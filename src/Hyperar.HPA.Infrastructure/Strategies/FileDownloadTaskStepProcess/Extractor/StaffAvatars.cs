namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.StaffAvatars;
    using Shared.Models.UI.Download;

    public class StaffAvatars : FileDownloadTaskStepProcessStrategyBase, IFileDownloadTaskStepProcessStrategy
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
                await Task.Delay(0, cancellationToken);

                if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
                {
                    ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.XmlFile, nameof(xmlFileDownloadTask.XmlFile));

                    if (xmlFileDownloadTask.XmlFile is HattrickData file)
                    {
                        foreach (Staff staff in file.StaffMembers)
                        {
                            if (!ImageFileExists(staff.Avatar.BackgroundImage))
                            {
                                fileDownloadTasks.Add(
                                    new ImageFileDownloadTask(
                                        staff.Avatar.BackgroundImage));
                            }

                            foreach (Layer layer in staff.Avatar.Layers)
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