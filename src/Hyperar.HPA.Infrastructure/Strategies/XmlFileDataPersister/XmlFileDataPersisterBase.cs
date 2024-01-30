namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;

    public abstract class XmlFileDataPersisterBase : IXmlFileDataPersisterStrategy
    {
        private const string host = "www.hattrick.org";

        private const string scheme = "https";

        public abstract Task PersistDataAsync(IXmlFile file);

        protected static async Task<byte[]> BuildAvatarFromLayers(ICollection<Domain.StaffMemberAvatarLayer> layers)
        {
            ArgumentNullException.ThrowIfNull(layers, nameof(layers));

            var firstLayer = layers.Single(x => x.Index == 1);

            var firstLayerImage = GetImageFromBytes(
                await DownloadWebResourceAsync(layers.Single(x => x.Index == 1).ImageUrl));

            var avatarImage = new Bitmap(firstLayerImage.Width, firstLayerImage.Height, PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                firstLayerImage,
                firstLayer.XCoordinate,
                firstLayer.YCoordinate,
                firstLayerImage.Width,
                firstLayerImage.Height);

            for (int i = 2; i < layers.Count + 1; i++)
            {
                var curLayer = layers.Single(x => x.Index == i);

                var layerImage = GetImageFromBytes(
                    await DownloadWebResourceAsync(curLayer.ImageUrl));

                graphics.DrawImage(
                    layerImage,
                    curLayer.XCoordinate,
                    curLayer.YCoordinate,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static async Task<byte[]> BuildAvatarFromLayers(ICollection<Domain.ManagerAvatarLayer> layers)
        {
            ArgumentNullException.ThrowIfNull(layers, nameof(layers));

            var firstLayer = layers.Single(x => x.Index == 1);

            var firstLayerImage = GetImageFromBytes(
                await DownloadWebResourceAsync(layers.Single(x => x.Index == 1).ImageUrl));

            var avatarImage = new Bitmap(firstLayerImage.Width, firstLayerImage.Height, PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                firstLayerImage,
                firstLayer.XCoordinate,
                firstLayer.YCoordinate,
                firstLayerImage.Width,
                firstLayerImage.Height);

            for (int i = 2; i < layers.Count + 1; i++)
            {
                var curLayer = layers.Single(x => x.Index == i);

                var layerImage = GetImageFromBytes(
                    await DownloadWebResourceAsync(curLayer.ImageUrl));

                graphics.DrawImage(
                    layerImage,
                    curLayer.XCoordinate,
                    curLayer.YCoordinate,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static async Task<byte[]> BuildAvatarFromLayers(ICollection<Domain.PlayerAvatarLayer> layers)
        {
            ArgumentNullException.ThrowIfNull(layers, nameof(layers));

            var firstLayer = layers.Single(x => x.Index == 1);

            var firstLayerImage = GetImageFromBytes(
                await DownloadWebResourceAsync(layers.Single(x => x.Index == 1).ImageUrl));

            var avatarImage = new Bitmap(firstLayerImage.Width, firstLayerImage.Height, PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                firstLayerImage,
                firstLayer.XCoordinate,
                firstLayer.YCoordinate,
                firstLayerImage.Width,
                firstLayerImage.Height);

            for (int i = 2; i < layers.Count + 1; i++)
            {
                var curLayer = layers.Single(x => x.Index == i);

                var layerImage = GetImageFromBytes(
                    await DownloadWebResourceAsync(curLayer.ImageUrl));

                graphics.DrawImage(
                    layerImage,
                    curLayer.XCoordinate,
                    curLayer.YCoordinate,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static async Task<byte[]> DownloadWebResourceAsync(string url)
        {
            var fileContent = await TryGetFileBytesFromCacheAsync(url);

            if (fileContent != null)
            {
                return fileContent;
            }

            using (var httpClient = new HttpClient())
            {
                fileContent = await httpClient.GetByteArrayAsync(
                    NormalizeUrl(url));
            }

            await WriteFileToCacheAsync(url, fileContent);

            return fileContent;
        }

        protected static string NormalizeUrl(string rawUrl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(rawUrl, nameof(rawUrl));

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

        private static byte[] GetBytesFromImage(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();
            }
        }

        private static string GetFilePathFromUrl(string url)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));

            string relativePath = new Uri(url).LocalPath.Replace("/", "\\");

            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "Hyperar",
                "HPA") + relativePath;
        }

        private static Bitmap GetImageFromBytes(byte[] imageBytes)
        {
            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var bitmap = new Bitmap(Image.FromStream(memoryStream));

                bitmap.SetResolution(120, 120);

                return bitmap;
            }
        }

        private static async Task<byte[]?> TryGetFileBytesFromCacheAsync(string url)
        {
            string filePath = GetFilePathFromUrl(url);

            return File.Exists(filePath)
                ? await File.ReadAllBytesAsync(filePath)
                : null;
        }

        private static async Task WriteFileToCacheAsync(string url, byte[] fileContent)
        {
            string filePath = GetFilePathFromUrl(url);

            if (!Directory.Exists(filePath.Substring(0, filePath.LastIndexOf('\\'))))
            {
                Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf('\\')));
            }

            await File.WriteAllBytesAsync(filePath, fileContent);
        }
    }
}