namespace Hyperar.HPA.Infrastructure.Features.Download.Download.Strategies
{
    using System.Net;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Shared.Enums;

    public class ImageFile : DownloadTaskStrategyBase, IDownloaderStrategy
    {
        public async Task DownloadAsync(DownloadTaskBase task, CancellationToken cancellationToken)
        {
            if (task is ImageDownloadTask imageDownloadTask)
            {
                task.Status = DownloadTaskStatus.Downloading;

                try
                {
                    var responseBytes = await DownloadWebResourceAsync(imageDownloadTask.Url, cancellationToken);

                    await WriteFileToCacheAsync(imageDownloadTask.Url, responseBytes, cancellationToken);
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode != HttpStatusCode.NotFound)
                    {
                        throw;
                    }
                }

                task.Status = DownloadTaskStatus.Finished;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(task));
            }
        }
    }
}