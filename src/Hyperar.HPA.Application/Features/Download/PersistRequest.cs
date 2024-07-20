namespace Hyperar.HPA.Application.Features.Download
{
    using System;
    using System.Collections.Generic;
    using Application.Interfaces;
    using Shared.Models.UI.Download;

    public class PersistRequest : IDownloadRequest<DownloadTaskBase>
    {
        public PersistRequest(
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