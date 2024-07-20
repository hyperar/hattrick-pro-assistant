namespace Hyperar.HPA.Shared.Enums
{
    public enum DownloadTaskStatus : int
    {
        Pending,

        Downloading,

        Downloaded,

        Parsing,

        Parsed,

        Processing,

        Processed,

        Persisting,

        Finished,

        Error,

        Canceled,

        Skipped
    }
}