namespace Hyperar.HPA.Shared.Models.UI.Download
{
    using Shared.Enums;

    public class TaskRow
    {
        public TaskRow(
            string title,
            DownloadTaskType type,
            DownloadTaskStatus status)
        {
            this.Title = title;
            this.Type = type;
            this.Status = status;
        }

        public DownloadTaskStatus Status { get; }

        public string Title { get; set; }

        public DownloadTaskType Type { get; set; }
    }
}