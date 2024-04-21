namespace Hyperar.HPA.Application.Models
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;

    public class ImageFileDownloadTask : FileDownloadTaskBase, IFileDownloadTask, IImageFileDownloadTask
    {
        private const string host = "www.hattrick.org";

        private const string scheme = "https";

        public ImageFileDownloadTask(string imageUrl) : base(DownloadTaskType.ImageFile, imageUrl.Substring(imageUrl.LastIndexOf('/') + 1))
        {
            this.ImageUrl = NormalizeUrl(imageUrl);
        }

        public string ImageUrl { get; }

        private static string NormalizeUrl(string rawUrl)
        {
            ArgumentException.ThrowIfNullOrEmpty(rawUrl, nameof(rawUrl));

            rawUrl = rawUrl.Trim().ToLower();

            if (rawUrl.StartsWith("//"))
            {
                rawUrl = $"{scheme}:{rawUrl}";
            }
            else if (rawUrl.StartsWith("/img/"))
            {
                rawUrl = $"{scheme}://{host}{rawUrl}";
            }
            else if (!rawUrl.StartsWith("http://") && !rawUrl.StartsWith("https://"))
            {
                rawUrl = $"{scheme}://{rawUrl}";
            }

            return Uri.TryCreate(rawUrl, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = true }, out Uri? rawUri)
                ? rawUri.ToString()
                : throw new ArgumentException(rawUrl, nameof(rawUrl));
        }
    }
}