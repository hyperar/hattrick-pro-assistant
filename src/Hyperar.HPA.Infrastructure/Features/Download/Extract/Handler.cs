namespace Hyperar.HPA.Infrastructure.Features.Download.Extract
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Hyperar.HPA.Application.Features.Download;
    using MediatR;
    using Shared.Enums;

    public class Handler : IRequestHandler<ExtractRequest>
    {
        private readonly IExtractorFactory extractorFactory;

        public Handler(IExtractorFactory extractorFactory)
        {
            this.extractorFactory = extractorFactory;
        }

        public async Task Handle(ExtractRequest request, CancellationToken cancellationToken)
        {
            if (request.Task is XmlDownloadTask xmlDownloadTask)
            {
                Services.DownloadViewService.ReportProgress(
                    request.Task,
                    request.TaskList,
                    true,
                    request.Progress);

                request.Task.Status = DownloadTaskStatus.Processing;

                var extractor = this.extractorFactory.GetExtractor(xmlDownloadTask);

                await extractor.ExtractAsync(xmlDownloadTask, request.TaskList, request.DownloadSettings, cancellationToken);

                request.Task.Status = DownloadTaskStatus.Processed;

                Services.DownloadViewService.ReportProgress(
                    request.Task,
                    request.TaskList,
                    true,
                    request.Progress);
            }
            else
            {
                throw new ArgumentException(nameof(request));
            }
        }
    }
}