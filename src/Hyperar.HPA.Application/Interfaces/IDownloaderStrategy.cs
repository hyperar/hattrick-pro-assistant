namespace Hyperar.HPA.Application.Interfaces
{
    public interface IDownloaderStrategy
    {
        Task DownloadAsync(DownloadTaskBase task, CancellationToken cancellationToken);
    }
}