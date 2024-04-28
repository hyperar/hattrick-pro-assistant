namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;
    using Strategies.FileDownloadTaskStepProcess.Downloader;

    public class ImageFileDownloadTaskStepProcessFactory : IImageFileDownloadTaskStepProcessFactory
    {
        private readonly ImageFile imageFileDownloader;

        public ImageFileDownloadTaskStepProcessFactory(ImageFile imageFileDownloader)
        {
            this.imageFileDownloader = imageFileDownloader;
        }

        public IFileDownloadTaskStepProcessStrategy GetDownloadTaskStepProcess(IFileDownloadTask fileDownloadTask)
        {
            try
            {
                return fileDownloadTask is IImageFileDownloadTask imageFileDownloadTask
                     ? (IFileDownloadTaskStepProcessStrategy)(imageFileDownloadTask.Status switch
                     {
                         DownloadTaskStatus.NotStarted => this.imageFileDownloader,
                         _ => throw new ArgumentOutOfRangeException(nameof(fileDownloadTask), fileDownloadTask.Status.ToString(), nameof(fileDownloadTask.Status))
                     })
                     : throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
            }
            catch
            {
                fileDownloadTask.Status = DownloadTaskStatus.Error;

                throw;
            }
        }
    }
}