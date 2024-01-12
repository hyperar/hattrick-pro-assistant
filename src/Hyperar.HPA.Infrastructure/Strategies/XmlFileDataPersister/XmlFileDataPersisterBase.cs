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

        protected static async Task<byte[]> BuildAvatarFromLayers(ICollection<Domain.ManagerAvatarLayer> layers)
        {
            ArgumentNullException.ThrowIfNull(layers, nameof(layers));

            var firstLayer = layers.Single(x => x.Index == 1);

            var firstLayerImage = GetImageFromBytes(
                await DownloadWebResource(layers.Single(x => x.Index == 1).ImageUrl));

            var avatarImage = new Bitmap(firstLayerImage.Width, firstLayerImage.Height, PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                firstLayerImage,
                firstLayer.XCoordinate,
                firstLayer.YCoordinate,
                firstLayerImage.Width,
                firstLayerImage.Height);

            for (int i = 2; i < layers.Count; i++)
            {
                var curLayer = layers.Single(x => x.Index == i);

                var layerImage = GetImageFromBytes(
                    await DownloadWebResource(curLayer.ImageUrl));

                graphics.DrawImage(
                    layerImage,
                    curLayer.XCoordinate,
                    curLayer.YCoordinate,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static async Task<byte[]> BuildAvatarFromLayers(ICollection<Domain.SeniorPlayerAvatarLayer> layers)
        {
            ArgumentNullException.ThrowIfNull(layers, nameof(layers));

            var firstLayer = layers.Single(x => x.Index == 1);

            var firstLayerImage = GetImageFromBytes(
                await DownloadWebResource(layers.Single(x => x.Index == 1).ImageUrl));

            var avatarImage = new Bitmap(firstLayerImage.Width, firstLayerImage.Height, PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                firstLayerImage,
                firstLayer.XCoordinate,
                firstLayer.YCoordinate,
                firstLayerImage.Width,
                firstLayerImage.Height);

            for (int i = 2; i < layers.Count; i++)
            {
                var curLayer = layers.Single(x => x.Index == i);

                var layerImage = GetImageFromBytes(
                    await DownloadWebResource(curLayer.ImageUrl));

                graphics.DrawImage(
                    layerImage,
                    curLayer.XCoordinate,
                    curLayer.YCoordinate,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static async Task<byte[]> DownloadWebResource(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetByteArrayAsync(
                    NormalizeUrl(url));
            }
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

        private static Bitmap GetImageFromBytes(byte[] imageBytes)
        {
            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var bitmap = new Bitmap(Image.FromStream(memoryStream));

                bitmap.SetResolution(120, 120);

                return bitmap;
            }
        }
    }
}