namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;

    public abstract class XmlFileDataPersisterBase : IXmlFileDataPersisterStrategy
    {
        public abstract Task PersistDataAsync(IXmlFile file);

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
                rawUrl = "http:" + rawUrl;
            }
            else if (!rawUrl.StartsWith("http://") && !rawUrl.StartsWith("https://"))
            {
                rawUrl = "http://" + rawUrl;
            }

            return Uri.TryCreate(rawUrl, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = true }, out Uri? rawUri)
                ? rawUri.ToString()
                : throw new ArgumentException(rawUrl, nameof(rawUrl));
        }
    }
}