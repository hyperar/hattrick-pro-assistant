namespace Hyperar.HPA.Infrastructure
{
    using Application;
    using Application.Interfaces;
    using Hyperar.HPA.Application.Features.Download;
    using Shared.Enums;
    using Shared.Models.UI.Download;

    public class DownloadRequestFactory : IDownloadRequestFactory
    {
        public IDownloadRequest<DownloadTaskBase> Create(
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress)
        {
            var firstUncompletedTask = taskList.First(x => x.Count > x.CompletedCount);

            if (firstUncompletedTask.HasPendingChildTasks)
            {
                return this.CreateDownloadTaskRequest(
                    firstUncompletedTask.ChildImageTaskList.First(x => x.Status == DownloadTaskStatus.Pending),
                    taskList,
                    progress);
            }
            else
            {
                return this.CreateDownloadTaskRequest(firstUncompletedTask, taskList, downloadSettings, progress);
            }
        }

        private IDownloadRequest<DownloadTaskBase> CreateDownloadTaskRequest(
            ImageDownloadTask task,
            IList<XmlDownloadTask> taskList,
            IProgress<ProcessReport> progress)
        {
            return task.Status switch
            {
                DownloadTaskStatus.Pending => new DownloadRequest(task, taskList, progress),
                _ => throw new ArgumentOutOfRangeException(nameof(task.Status))
            };
        }

        private IDownloadRequest<DownloadTaskBase> CreateDownloadTaskRequest(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress)
        {
            return task.Status switch
            {
                DownloadTaskStatus.Pending => new DownloadRequest(task, taskList, progress),
                DownloadTaskStatus.Downloaded => new ParseRequest(task, taskList, progress),
                DownloadTaskStatus.Parsed => new ExtractRequest(task, taskList, downloadSettings, progress),
                DownloadTaskStatus.Processed => new PersistRequest(task, taskList, progress),
                _ => throw new ArgumentOutOfRangeException(nameof(task.Status))
            };
        }
    }
}