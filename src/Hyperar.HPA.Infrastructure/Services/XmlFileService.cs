namespace Hyperar.HPA.Infrastructure.Services
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Application.Models;
    using Application.Services;
    using Common.ExtensionMethods;
    using Domain.Interfaces;

    public class XmlFileService : IXmlFileService
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IXmlDownloadTaskExtractorFactory downloadTaskExtractorFactory;

        private readonly IXmlEntityFactory entityFactory;

        private readonly IXmlFileDataPersisterFactory fileDataPersisterFactory;

        private readonly IXmlFileParserFactory fileParserFactory;

        public XmlFileService(
            IDatabaseContext databaseContext,
            IXmlDownloadTaskExtractorFactory downloadTaskExtractorFactory,
            IXmlEntityFactory entityFactory,
            IXmlFileDataPersisterFactory fileDataPersisterFactory,
            IXmlFileParserFactory fileParserFactory)
        {
            this.databaseContext = databaseContext;
            this.downloadTaskExtractorFactory = downloadTaskExtractorFactory;
            this.entityFactory = entityFactory;
            this.fileDataPersisterFactory = fileDataPersisterFactory;
            this.fileParserFactory = fileParserFactory;
        }

        public async Task BeginPersistSession()
        {
            await this.databaseContext.BeginTransactionAsync();
        }

        public async Task CancelPersistSession()
        {
            await Task.Run(() => this.databaseContext.Cancel());
        }

        public async Task EndPersistSession()
        {
            await this.databaseContext.EndTransactionAsync();
        }

        public DownloadTask[] ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            IXmlDownloadTaskExtractorStrategy childTaskBuilder = this.downloadTaskExtractorFactory.CreateDownloadTaskExtractor(xmlFile.FileName.ToXmlFileType());

            return childTaskBuilder.ExtractXmlDownloadTasks(xmlFile);
        }

        public async Task<IXmlFile> ParseFileAsync(Stream fileStream)
        {
            IXmlFile result;

            using (XmlReader reader = XmlReader.Create(
                fileStream,
                new XmlReaderSettings
                {
                    Async = true,
                    CloseInput = true,
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    IgnoreWhitespace = true
                }))
            {
                reader.ReadToFollowing("FileName");

                result = this.entityFactory.CreateEntity(await reader.ReadElementContentAsStringAsync());

                IXmlFileParserStrategy parser = this.fileParserFactory.CreateXmlFileParser(result.FileName.ToXmlFileType());

                result = await parser.ParseAsync(reader, result);
            }

            return result;
        }

        public async Task<IXmlFile> ParseFileAsync(string fileContent)
        {
            MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(fileContent));

            return await this.ParseFileAsync(memoryStream);
        }

        public async Task PersistFileAsync(IXmlFile xmlFile)
        {
            IXmlFileDataPersisterStrategy persister = this.fileDataPersisterFactory.GetPersister(xmlFile.FileName.ToXmlFileType());

            await persister.PersistDataAsync(xmlFile);
        }

        public async Task PersistFileAsync(IXmlFile xmlFile, uint contextId)
        {
            IXmlFileDataPersisterStrategy persister = this.fileDataPersisterFactory.GetPersister(xmlFile.FileName.ToXmlFileType());

            await persister.PersistDataWithContextAsync(xmlFile, contextId);
        }
    }
}