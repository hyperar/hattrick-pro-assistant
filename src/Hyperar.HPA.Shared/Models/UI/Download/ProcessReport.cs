namespace Hyperar.HPA.Shared.Models.UI.Download
{
    using System.Collections.Generic;

    public class ProcessReport
    {
        public ProcessReport(
            ICollection<TaskRow> rows,
            TaskRow currentTask,
            bool isDownloading,
            int taskCount,
            int completedTaskCount)
        {
            this.Rows = rows;
            this.CurrentTask = currentTask;
            this.IsDownloading = isDownloading;
            this.TaskCount = taskCount;
            this.CompletedTaskCount = completedTaskCount;
        }

        public bool CanDownload
        {
            get
            {
                return !this.IsDownloading;
            }
        }

        public int CompletedTaskCount { get; }

        public TaskRow CurrentTask { get; set; }

        public bool IsDownloading { get; }

        public ICollection<TaskRow> Rows { get; }

        public int TaskCount { get; }
    }
}