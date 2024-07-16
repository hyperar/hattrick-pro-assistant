namespace Hyperar.HPA.Application.Features
{
    using System;
    using System.Collections.Generic;
    using Application.Interfaces;
    using Shared.Models.UI.Download;

    public class ImageFileDownloadRequest : IDownloadRequest<ImageDownloadTask>
    {
        public ImageFileDownloadRequest(
            ImageDownloadTask task,
            IList<XmlDownloadTask> taskList,
            IProgress<ProcessReport> progress)
        {
            this.Task = task;
            this.TaskList = taskList;
            this.Progress = progress;
        }

        public IProgress<ProcessReport> Progress { get; }

        public ImageDownloadTask Task { get; }

        public IList<XmlDownloadTask> TaskList { get; }
    }
}