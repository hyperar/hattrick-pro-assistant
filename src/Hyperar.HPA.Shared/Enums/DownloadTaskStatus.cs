namespace Hyperar.HPA.Shared.Enums
{
    public enum DownloadTaskStatus : byte
    {
        NotStarted = 0,

        Downloaded = 1,

        Parsed = 2,

        Processed = 3,

        Finished = 4,

        Error = 5,

        Canceled = 6
    }
}