namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.UI.Download;
    using Models = Shared.Models.Hattrick;

    public abstract class PersisterBase : IFileDownloadTaskStepProcessStrategy
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
                if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
                {
                    await this.PersistFileAsync(xmlFileDownloadTask, cancellationToken);
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

        public abstract Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken);

        protected static async Task<byte[]> BuildAvatarFromLayersAsync(Models.Avatar avatar)
        {
            Bitmap backgroundImage = await CreateAvatarImageAsync(avatar.BackgroundImage);

            Bitmap avatarImage = new Bitmap(
                backgroundImage.Width,
                backgroundImage.Height,
                PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                backgroundImage,
                0,
                0,
                backgroundImage.Width,
                backgroundImage.Height);

            for (int i = 1; i < avatar.Layers.Count; i++)
            {
                var layer = avatar.Layers[i];

                Bitmap layerImage = GetImageFromBytes(
                    await GetImageBytesFromCacheAsync(
                        layer.Image));

                graphics.DrawImage(
                    layerImage,
                    layer.X,
                    layer.Y,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static async Task<byte[]> GetImageBytesFromCacheAsync(string url, CancellationToken cancellationToken)
        {
            string filePath = GetFilePathFromUrl(url);

            return File.Exists(filePath)
                ? await File.ReadAllBytesAsync(filePath, cancellationToken)
                : throw new FileNotFoundException(
                    Globalization.Translations.CacheImageFileNotFound,
                    filePath);
        }

        protected static async Task<byte[]> GetImageBytesFromCacheAsync(string url)
        {
            string filePath = GetFilePathFromUrl(url);

            return File.Exists(filePath)
                ? await File.ReadAllBytesAsync(filePath)
                : throw new FileNotFoundException(
                    Globalization.Translations.CacheImageFileNotFound,
                    filePath);
        }

        protected static async Task<byte[]> GetImageBytesFromCacheAsync(string url, string fallBackUrl)
        {
            string filePath = GetFilePathFromUrl(url);
            string fallBackFilePath = GetFilePathFromUrl(fallBackUrl);

            return File.Exists(filePath)
                ? await File.ReadAllBytesAsync(filePath)
                : File.Exists(fallBackFilePath)
                ? await File.ReadAllBytesAsync(fallBackFilePath)
                : throw new FileNotFoundException(
                    Globalization.Translations.CacheImageFileNotFound,
                    filePath);
        }

        private static async Task<Bitmap> CreateAvatarImageAsync(string url)
        {
            return GetImageFromBytes(
                await GetImageBytesFromCacheAsync(
                    url));
        }

        private static byte[] GetBytesFromImage(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();
            }
        }

        private static string GetFilePathFromUrl(string url)
        {
            ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));

            string normalizedUrl = NormalizeUrl(url);

            string relativePath = new Uri(normalizedUrl).LocalPath.Replace("/", "\\");

            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "Hyperar",
                "HPA") + relativePath;
        }

        private static Bitmap GetImageFromBytes(byte[] imageBytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                Bitmap bitmap = new Bitmap(Image.FromStream(memoryStream));

                bitmap.SetResolution(120, 120);

                return bitmap;
            }
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
    }
}