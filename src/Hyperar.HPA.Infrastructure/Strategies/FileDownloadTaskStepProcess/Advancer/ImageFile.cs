namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Advancer
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;

    public class ImageFile : IFileDownloadTaskStepAdvancerStrategy
    {
        public void AdvanceTaskStatus(IFileDownloadTask fileDownloadTask)
        {
            if (fileDownloadTask is IImageFileDownloadTask imageFileDownloadTask)
            {
                fileDownloadTask.Status = imageFileDownloadTask.Status switch
                {
                    DownloadTaskStatus.NotStarted => DownloadTaskStatus.Finished,
                    _ => throw new ArgumentOutOfRangeException(nameof(fileDownloadTask), fileDownloadTask.Status.ToString(), nameof(imageFileDownloadTask.Status))
                };
            }
            else
            {
                throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
            }
        }
    }
}