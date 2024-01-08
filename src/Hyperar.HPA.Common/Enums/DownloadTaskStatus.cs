namespace Hyperar.HPA.Common.Enums
{
    public enum DownloadTaskStatus : uint
    {
        Pending = 0,

        Downloading = 1,

        Processing = 2,

        Saving = 3,

        Done = 4,

        Error = 5,

        Canceled = 6
    }
}