﻿namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Common.ExtensionMethods;

    public class XmlFileService : IXmlFileService
    {
        private readonly IXmlEntityFactory entityFactory;

        private readonly IXmlFileParserFactory fileParserFactory;

        private readonly IXmlDownloadTaskExtractorFactory xmlDownloadTaskExtractorFactory;

        private readonly IXmlFileDataPersisterFactory xmlFileDataPersisterFactory;

        public XmlFileService(
            IXmlFileParserFactory fileParserFactory,
            IXmlEntityFactory entityFactory,
            IXmlDownloadTaskExtractorFactory xmlDownloadTaskExtractorFactory,
            IXmlFileDataPersisterFactory xmlFileDataPersisterFactory)
        {
            this.fileParserFactory = fileParserFactory;
            this.entityFactory = entityFactory;
            this.xmlDownloadTaskExtractorFactory = xmlDownloadTaskExtractorFactory;
            this.xmlFileDataPersisterFactory = xmlFileDataPersisterFactory;
        }

        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            IXmlDownloadTaskExtractorStrategy childTaskBuilder = this.xmlDownloadTaskExtractorFactory.CreateDownloadTaskExtractor(xmlFile.FileName.ToXmlFileType());

            return childTaskBuilder.ExtractXmlDownloadTasks(xmlFile);
        }

        public IXmlFile ParseFile(Stream fileStream)
        {
            IXmlFile result;

            using (XmlReader reader = XmlReader.Create(
                fileStream,
                new XmlReaderSettings
                {
                    CloseInput = true,
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    IgnoreWhitespace = true
                }))
            {
                reader.ReadToFollowing("FileName");

                result = this.entityFactory.CreateEntity(reader.ReadElementContentAsString());

                IXmlFileParserStrategy parser = this.fileParserFactory.CreateXmlFileParser(result.FileName.ToXmlFileType());

                parser.Parse(reader, ref result);
            }

            return result;
        }

        public IXmlFile ParseFile(string fileContent)
        {
            MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(fileContent));

            return this.ParseFile(memoryStream);
        }

        public void Persist(IXmlFile xmlFile)
        {
            var persister = this.xmlFileDataPersisterFactory.GetPersister(xmlFile.FileName.ToXmlFileType());

            persister.PersistData(xmlFile);
        }
    }
}