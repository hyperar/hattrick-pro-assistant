namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;

    using Strategies.FileDownloadTaskStepProcess.Downloader;

    public class XmlFileDownloadTaskStepProcessFactory : IXmlFileDownloadTaskStepProcessFactory
    {
        private readonly IXmlFileDownloadTaskExtractStepProcessFactory extractStepProcessFactory;

        private readonly IXmlFileDownloadTaskParseStepProcessFactory parseStepProcessFactory;

        private readonly IXmlFileDownloadTaskPersistStepProcessFactory persistStepProcessFactory;

        private readonly XmlFile xmlFileDownloader;

        public XmlFileDownloadTaskStepProcessFactory(
            XmlFile xmlFileDownloader,
            IXmlFileDownloadTaskParseStepProcessFactory parseStepProcessFactory,
            IXmlFileDownloadTaskExtractStepProcessFactory extractStepProcessFactory,
            IXmlFileDownloadTaskPersistStepProcessFactory persistStepProcessFactory)
        {
            this.xmlFileDownloader = xmlFileDownloader;
            this.parseStepProcessFactory = parseStepProcessFactory;
            this.extractStepProcessFactory = extractStepProcessFactory;
            this.persistStepProcessFactory = persistStepProcessFactory;
        }

        public IFileDownloadTaskStepProcessStrategy GetDownloadTaskStepProcess(IFileDownloadTask fileDownloadTask)
        {
            try
            {
                if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
                {
                    return xmlFileDownloadTask.Status switch
                    {
                        DownloadTaskStatus.NotStarted => this.xmlFileDownloader,
                        DownloadTaskStatus.Downloaded => this.parseStepProcessFactory.GetParser(xmlFileDownloadTask),
                        DownloadTaskStatus.Parsed => this.extractStepProcessFactory.GetExtractor(xmlFileDownloadTask),
                        DownloadTaskStatus.Processed => this.persistStepProcessFactory.GetPersister(xmlFileDownloadTask),
                        _ => throw new ArgumentOutOfRangeException(nameof(fileDownloadTask), fileDownloadTask.Status.ToString(), nameof(fileDownloadTask.Status))
                    };
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