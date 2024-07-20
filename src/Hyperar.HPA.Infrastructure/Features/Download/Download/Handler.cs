namespace Hyperar.HPA.Infrastructure.Features.Download.Download
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Hyperar.HPA.Application.Features.Download;
    using MediatR;
    using Shared.Enums;

    public class Handler : IRequestHandler<DownloadRequest>
    {
        private readonly IDownloaderFactory downloaderFactory;

        public Handler(IDownloaderFactory downloaderFactory)
        {
            this.downloaderFactory = downloaderFactory;
        }

        public async Task Handle(DownloadRequest request, CancellationToken cancellationToken)
        {
            request.Task.Status = DownloadTaskStatus.Downloading;

            Services.DownloadViewService.ReportProgress(
                request.Task,
                request.TaskList,
                true,
                request.Progress);

            var downloader = this.downloaderFactory.GetDownloader(request.Task);

            await downloader.DownloadAsync(request.Task, cancellationToken);

            if (request.Task is ImageDownloadTask)
            {
                request.Task.Status = DownloadTaskStatus.Finished;
            }
            else
            {
                request.Task.Status = DownloadTaskStatus.Downloaded;
            }

            Services.DownloadViewService.ReportProgress(
                request.Task,
                request.TaskList,
                true,
                request.Progress);
        }
    }
}