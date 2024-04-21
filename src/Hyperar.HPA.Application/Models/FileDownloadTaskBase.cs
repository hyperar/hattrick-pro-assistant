namespace Hyperar.HPA.Application.Models
{
    using Application.Interfaces;
    using Shared.Enums;

    public abstract class FileDownloadTaskBase : IFileDownloadTask
    {
        protected FileDownloadTaskBase(DownloadTaskType type, string title)
        {
            this.Type = type;
            this.Title = title;
        }

        public DownloadTaskStatus Status { get; set; }

        public string Title { get; }

        public DownloadTaskType Type { get; }
    }
}