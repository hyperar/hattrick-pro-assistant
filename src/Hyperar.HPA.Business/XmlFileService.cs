namespace Hyperar.HPA.Business
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.ExtensionMethods;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Hattrick;

    public class XmlFileService : IXmlFileService
    {
        private readonly IXmlFileParserFactory fileParserFactory;

        private readonly IXmlEntityFactory entityFactory;

        private readonly IXmlChildDownloadTaskBuilderFactory childTaskBuilderFactory;

        public XmlFileService(
            IXmlFileParserFactory fileParserFactory,
            IXmlEntityFactory entityFactory,
            IXmlChildDownloadTaskBuilderFactory childTaskBuilderFactory)
        {
            this.fileParserFactory = fileParserFactory;
            this.entityFactory = entityFactory;
            this.childTaskBuilderFactory = childTaskBuilderFactory;
        }

        public List<DownloadTask>? GetChildDownloadTaskList(XmlFileBase xmlFile)
        {
            var childTaskBuilder = this.childTaskBuilderFactory.CreateChildTaskBuilder(xmlFile.FileName.ToXmlFileType());

            return childTaskBuilder.BuildChildDownloadTaskList(xmlFile);
        }

        public XmlFileBase ParseFile(Stream fileStream)
        {
            XmlFileBase result;

            using (var reader = XmlReader.Create(
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

                var parser = this.fileParserFactory.CreateXmlFileParser(result.FileName.ToXmlFileType());

                parser.Parse(reader, ref result);
            }

            return result;
        }

        public XmlFileBase ParseFile(string fileContent)
        {
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));

            return this.ParseFile(memoryStream);
        }
    }
}
