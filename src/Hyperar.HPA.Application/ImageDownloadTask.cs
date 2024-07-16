namespace Hyperar.HPA.Application
{
    using Shared.Enums;

    public class ImageDownloadTask : DownloadTaskBase
    {
        public ImageDownloadTask(string url) : base(url.Substring(url.LastIndexOf('/') + 1))
        {
            this.Url = url;
            this.Type = DownloadTaskType.ImageFile;
        }

        public string Url { get; set; }
    }
}