namespace Hyperar.HPA.Application.Features.Download
{
    using System;
    using Application.Interfaces;
    using Shared.Models.UI.Download;

    public class ExtractRequest : IDownloadRequest<DownloadTaskBase>
    {
        public ExtractRequest(
            DownloadTaskBase task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress)
        {
            this.Task = task;
            this.TaskList = taskList;
            this.DownloadSettings = downloadSettings;
            this.Progress = progress;
        }

        public DownloadSettings DownloadSettings { get; }

        public IProgress<ProcessReport> Progress { get; }

        public DownloadTaskBase Task { get; }

        public IList<XmlDownloadTask> TaskList { get; }
    }
}