namespace Hyperar.HPA.Infrastructure.Features.Download.Parse
{
    using System.Text;
    using System.Xml;
    using Application;
    using Application.Features;
    using Application.Interfaces;
    using Constants;
    using MediatR;
    using Services;
    using Shared.Enums;
    using Shared.Models.Hattrick.Interfaces;
    using Strategies.ExtensionMethods;

    public class Handler : IRequestHandler<ParseRequest>
    {
        private readonly IXmlEntityFactory entityFactory;

        private readonly IParserFactory parserFactory;

        public Handler(IXmlEntityFactory entityFactory, IParserFactory parserFactory)
        {
            this.entityFactory = entityFactory;
            this.parserFactory = parserFactory;
        }

        public async Task Handle(ParseRequest request, CancellationToken cancellationToken)
        {
            if (request.Task is XmlDownloadTask xmlDownloadTask)
            {
                request.Task.Status = DownloadTaskStatus.Parsing;

                DownloadViewService.ReportProgress(
                    request.Task,
                    request.TaskList,
                    true,
                    request.Progress);

                ArgumentException.ThrowIfNullOrWhiteSpace(xmlDownloadTask.Response, nameof(xmlDownloadTask.Response));

                IXmlFile result;

                using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlDownloadTask.Response)))
                {
                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
                    {
                        Async = true,
                        CloseInput = true,
                        IgnoreComments = true,
                        IgnoreProcessingInstructions = true,
                        IgnoreWhitespace = true
                    };

                    using (var reader = XmlReader.Create(memoryStream, xmlReaderSettings))
                    {
                        reader.ReadToFollowing(NodeName.FileName);

                        result = this.entityFactory.CreateEntity(await reader.ReadElementContentAsStringAsync());

                        result.Version = await reader.ReadXmlValueAsDecimalAsync();
                        result.UserId = await reader.ReadXmlValueAsLongAsync();
                        result.FetchedDate = await reader.ReadXmlValueAsDateTimeAsync();

                        var parser = this.parserFactory.GetParser(xmlDownloadTask);

                        await parser.ParseAsync(reader, result, cancellationToken);

                        xmlDownloadTask.XmlFile = result;
                    }

                    request.Task.Status = DownloadTaskStatus.Parsed;

                    DownloadViewService.ReportProgress(
                        request.Task,
                        request.TaskList,
                        true,
                        request.Progress);
                }
            }
            else
            {
                throw new ArgumentException(nameof(request));
            }
        }
    }
}