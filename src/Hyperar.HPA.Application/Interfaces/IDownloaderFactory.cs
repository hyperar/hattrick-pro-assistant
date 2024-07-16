namespace Hyperar.HPA.Application.Interfaces
{
    public interface IDownloaderFactory
    {
        IDownloaderStrategy GetDownloader(DownloadTaskBase task);
    }
}