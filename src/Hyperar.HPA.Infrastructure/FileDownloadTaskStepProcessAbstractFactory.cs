namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;

    public class FileDownloadTaskStepProcessAbstractFactory : IFileDownloadTaskStepProcessAbstractFactory
    {
        private readonly IImageFileDownloadTaskStepProcessFactory imageFileDownloadTaskStepProcessFactory;

        private readonly IXmlFileDownloadTaskStepProcessFactory xmlFileDownloadTaskStepProcessFactory;

        public FileDownloadTaskStepProcessAbstractFactory(
            IImageFileDownloadTaskStepProcessFactory imageFileDownloadTaskStepProcessFactory,
            IXmlFileDownloadTaskStepProcessFactory xmlFileDownloadTaskStepProcessFactory)
        {
            this.imageFileDownloadTaskStepProcessFactory = imageFileDownloadTaskStepProcessFactory;
            this.xmlFileDownloadTaskStepProcessFactory = xmlFileDownloadTaskStepProcessFactory;
        }

        public IFileDownloadTaskStepProcessStrategy GetDownloadTaskStepProcess(IFileDownloadTask fileDownloadTask)
        {
            return fileDownloadTask switch
            {
                IImageFileDownloadTask imageFileDownload => this.imageFileDownloadTaskStepProcessFactory.GetDownloadTaskStepProcess(fileDownloadTask),
                IXmlFileDownloadTask xmlFileDownloadTask => this.xmlFileDownloadTaskStepProcessFactory.GetDownloadTaskStepProcess(fileDownloadTask),
                _ => throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType)
            };
        }
    }
}