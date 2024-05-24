namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Infrastructure.Strategies.FileDownloadTaskStepProcess.Advancer;

    public class FileDownloadTaskStepAdvancerFactory : IFileDownloadTaskStepAdvancerFactory
    {
        private readonly ImageFile imageFileAdvancer;

        private readonly XmlFile xmlFileAdvancer;

        public FileDownloadTaskStepAdvancerFactory(ImageFile imageFileAdvancer, XmlFile xmlFileAdvancer)
        {
            this.imageFileAdvancer = imageFileAdvancer;
            this.xmlFileAdvancer = xmlFileAdvancer;
        }

        public IFileDownloadTaskStepAdvancerStrategy GetAdvancer(IFileDownloadTask fileDownloadTask)
        {
            return fileDownloadTask switch
            {
                IImageFileDownloadTask => this.imageFileAdvancer,
                IXmlFileDownloadTask xmlFileDownloadTask => this.xmlFileAdvancer,
                _ => throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType)
            };
        }
    }
}