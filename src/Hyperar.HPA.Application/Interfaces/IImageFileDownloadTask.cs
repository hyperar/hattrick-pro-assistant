namespace Hyperar.HPA.Application.Interfaces
{
    public interface IImageFileDownloadTask : IFileDownloadTask
    {
        public string ImageUrl { get; }
    }
}