namespace Hyperar.HPA.Common.Enums
{
    public enum DownloadTaskStatus : byte
    {
        Pending = 0,

        Downloading = 1,

        Parsing = 2,

        Extracting = 3,

        Saving = 4,

        Done = 5,

        Error = 6,

        Canceled = 7
    }
}