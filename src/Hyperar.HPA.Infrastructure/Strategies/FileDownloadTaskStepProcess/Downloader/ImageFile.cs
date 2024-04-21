namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Downloader
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.UI.Download;

    public class ImageFile : IFileDownloadTaskStepProcessStrategy
    {
        private const string host = "www.hattrick.org";

        private const string scheme = "https";

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            ICollection<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                if (fileDownloadTask is IImageFileDownloadTask imageFileDownloadTask)
                {
                    string url = NormalizeUrl(imageFileDownloadTask.ImageUrl);

                    byte[]? imageBytes = await GetFileBytesFromCacheAsync(url, cancellationToken);

                    if (imageBytes == null)
                    {
                        imageBytes = await DownloadWebResourceAsync(url, cancellationToken);

                        ArgumentNullException.ThrowIfNull(imageBytes, nameof(imageBytes));

                        await WriteFileToCacheAsync(url, imageBytes, cancellationToken);
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

        private static async Task<byte[]> DownloadWebResourceAsync(string url, CancellationToken cancellationToken)
        {
            byte[] fileBytes = Array.Empty<byte>();

            using (HttpClient httpClient = new HttpClient())
            {
                fileBytes = await httpClient.GetByteArrayAsync(url, cancellationToken);
            }

            return fileBytes;
        }

        private static async Task<byte[]?> GetFileBytesFromCacheAsync(string url, CancellationToken cancellationToken)
        {
            string filePath = GetFilePathFromUrl(url);

            return File.Exists(filePath)
                ? await File.ReadAllBytesAsync(filePath, cancellationToken)
                : null;
        }

        private static string GetFilePathFromUrl(string url)
        {
            ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));

            string relativePath = new Uri(url).LocalPath.Replace("/", "\\");

            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "Hyperar",
                "HPA") + relativePath;
        }

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

        private static async Task WriteFileToCacheAsync(string url, byte[] fileContent, CancellationToken cancellationToken)
        {
            string filePath = GetFilePathFromUrl(url);

            if (!Directory.Exists(filePath.Substring(0, filePath.LastIndexOf('\\'))))
            {
                Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf('\\')));
            }

            await File.WriteAllBytesAsync(filePath, fileContent, cancellationToken);
        }
    }
}