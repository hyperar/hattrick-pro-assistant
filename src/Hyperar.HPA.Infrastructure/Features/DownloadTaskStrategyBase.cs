namespace Hyperar.HPA.Infrastructure.Features
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using Models = Shared.Models.Hattrick;

    public abstract class DownloadTaskStrategyBase
    {
        private const string host = "www.hattrick.org";

        private const string scheme = "https";

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

            for (int i = 0; i < avatar.Layers.Count; i++)
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

        protected static async Task<byte[]> DownloadWebResourceAsync(string url, CancellationToken cancellationToken)
        {
            string finalUrl = NormalizeUrl(url);

            byte[] fileBytes = Array.Empty<byte>();

            using (HttpClient httpClient = new HttpClient())
            {
                fileBytes = await httpClient.GetByteArrayAsync(finalUrl, cancellationToken);
            }

            return fileBytes;
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

        protected static bool ImageFileExists(string url)
        {
            return File.Exists(
                GetFilePathFromUrl(
                    url));
        }

        protected static async Task WriteFileToCacheAsync(string url, byte[] fileContent, CancellationToken cancellationToken)
        {
            string filePath = GetFilePathFromUrl(url);

            if (!Directory.Exists(filePath.Substring(0, filePath.LastIndexOf('\\'))))
            {
                Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf('\\')));
            }

            await File.WriteAllBytesAsync(filePath, fileContent, cancellationToken);
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