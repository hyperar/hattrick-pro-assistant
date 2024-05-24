namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess
{
    public abstract class FileDownloadTaskStepProcessStrategyBase
    {
        private const string host = "www.hattrick.org";

        private const string scheme = "https";

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