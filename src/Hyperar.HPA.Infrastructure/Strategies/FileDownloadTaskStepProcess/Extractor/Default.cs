namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Models.UI.Download;

    public class Default : IFileDownloadTaskStepProcessStrategy
    {
        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            IList<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
        }
    }
}