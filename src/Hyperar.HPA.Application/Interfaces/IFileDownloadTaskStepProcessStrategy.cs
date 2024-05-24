namespace Hyperar.HPA.Application.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Application.Models;
    using Shared.Models.UI.Download;

    public interface IFileDownloadTaskStepProcessStrategy
    {
        Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            IList<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken);
    }
}