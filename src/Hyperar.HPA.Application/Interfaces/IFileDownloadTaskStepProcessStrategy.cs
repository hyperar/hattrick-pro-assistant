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
            ICollection<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken);
    }
}