namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Advancer
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;

    public class XmlFile : IFileDownloadTaskStepAdvancerStrategy
    {
        public void AdvanceTaskStatus(IFileDownloadTask fileDownloadTask)
        {
            if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
            {
                fileDownloadTask.Status = xmlFileDownloadTask.Status switch
                {
                    DownloadTaskStatus.NotStarted => DownloadTaskStatus.Downloaded,
                    DownloadTaskStatus.Downloaded => DownloadTaskStatus.Parsed,
                    DownloadTaskStatus.Parsed => DownloadTaskStatus.Processed,
                    DownloadTaskStatus.Processed => DownloadTaskStatus.Finished,
                    _ => throw new ArgumentOutOfRangeException(nameof(fileDownloadTask), fileDownloadTask.Status.ToString(), nameof(xmlFileDownloadTask.Status))
                };
            }
            else
            {
                throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
            }
        }
    }
}