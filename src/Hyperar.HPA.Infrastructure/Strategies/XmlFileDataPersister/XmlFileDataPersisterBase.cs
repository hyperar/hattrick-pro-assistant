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

        protected static byte[] BuildAvatarFromLayers(ICollection<Domain.ManagerAvatarLayer> layers)
        {
            var avatarImage = new Bitmap(110, 155, PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            foreach (var curLayer in layers.OrderBy(x => x.Index))
            {
                var layerImage = GetImageFromBytes(curLayer.Image);

                graphics.DrawImage(
                    layerImage,
                    (int)curLayer.XCoordinate,
                    (int)curLayer.YCoordinate,
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