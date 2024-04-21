namespace Hyperar.HPA.Application.Interfaces
{
    using Shared.Enums;

    public interface IFileDownloadTask
    {
        DownloadTaskStatus Status { get; set; }

        string Title { get; }

        DownloadTaskType Type { get; }
    }
}