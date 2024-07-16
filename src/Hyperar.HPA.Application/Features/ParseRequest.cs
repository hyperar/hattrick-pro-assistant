namespace Hyperar.HPA.Application.Features
{
    using System;
    using Application.Interfaces;
    using Shared.Models.UI.Download;

    public class ParseRequest : IDownloadRequest<DownloadTaskBase>
    {
        public ParseRequest(
            DownloadTaskBase task,
            IList<XmlDownloadTask> taskList,
            IProgress<ProcessReport> progress)
        {
            this.Task = task;
            this.TaskList = taskList;
            this.Progress = progress;
        }

        public IProgress<ProcessReport> Progress { get; }

        public DownloadTaskBase Task { get; }

        public IList<XmlDownloadTask> TaskList { get; }
    }
}