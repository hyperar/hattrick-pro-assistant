namespace Hyperar.HPA.Infrastructure.Features.Download.Download
{
    using System;
    using Application;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Download.Strategies;

    public class DownloaderFactory : IDownloaderFactory
    {
        private readonly ImageFile imageFile;

        private readonly XmlFile xmlFile;

        public DownloaderFactory(ImageFile imageFile, XmlFile xmlFile)
        {
            this.imageFile = imageFile;
            this.xmlFile = xmlFile;
        }

        public IDownloaderStrategy GetDownloader(DownloadTaskBase task)
        {
            if (task is ImageDownloadTask)
            {
                return this.imageFile;
            }
            else if (task is XmlDownloadTask)
            {
                return this.xmlFile;
            }
            else
            {
                throw new ArgumentException(nameof(task));
            }
        }
    }
}