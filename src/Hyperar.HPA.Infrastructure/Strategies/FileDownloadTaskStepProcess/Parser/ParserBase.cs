namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Application.Models;
    using Constants;
    using ExtensionMethods;
    using Shared.Enums;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.UI.Download;

    public abstract class ParserBase : IFileDownloadTaskStepProcessStrategy
    {
        private readonly IXmlEntityFactory entityFactory;

        public ParserBase(IXmlEntityFactory entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            ICollection<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
                {
                    ArgumentException.ThrowIfNullOrWhiteSpace(xmlFileDownloadTask.Response, nameof(xmlFileDownloadTask.Response));

                    IXmlFile result;

                    using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlFileDownloadTask.Response)))
                    {
                        XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
                        {
                            Async = true,
                            CloseInput = true,
                            IgnoreComments = true,
                            IgnoreProcessingInstructions = true,
                            IgnoreWhitespace = true
                        };

                        using (XmlReader reader = XmlReader.Create(memoryStream, xmlReaderSettings))
                        {
                            reader.ReadToFollowing(NodeName.FileName);

                            result = this.entityFactory.CreateEntity(await reader.ReadElementContentAsStringAsync());

                            result = await this.ParseAsync(reader, result);
                        }

                        xmlFileDownloadTask.XmlFile = result;
                    }
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

        public abstract Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity);

        protected static async Task<IdName> ParseIdNameNodeAsync(XmlReader reader)
        {
            IdName result = new IdName();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                result.Id = await reader.ReadXmlValueAsLongAsync();
                result.Name = await reader.ReadElementContentAsStringAsync();
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private async Task<IXmlFile> ParseAsync(XmlReader reader, IXmlFile result)
        {
            result.Version = await reader.ReadXmlValueAsDecimalAsync();
            result.UserId = await reader.ReadXmlValueAsLongAsync();
            result.FetchedDate = await reader.ReadXmlValueAsDateTimeAsync();

            result = await this.ParseFileTypeSpecificContentAsync(reader, result);

            return result;
        }
    }
}