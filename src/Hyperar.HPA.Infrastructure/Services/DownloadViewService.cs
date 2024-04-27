namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Application.Services;
    using Domain.Interfaces;
    using Shared.Enums;
    using Shared.Models.UI.Download;

    public class DownloadViewService : IDownloadViewService
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IFileDownloadTaskStepAdvancerFactory fileDownloadTaskStepAdvancerFactory;

        private readonly IFileDownloadTaskStepProcessAbstractFactory fileDownloadTaskStepProcessFactory;

        public DownloadViewService(
            IDatabaseContext databaseContext,
            IFileDownloadTaskStepAdvancerFactory fileDownloadTaskStepAdvancerFactory,
            IFileDownloadTaskStepProcessAbstractFactory fileDownloadTaskStepProcessFactory)
        {
            this.databaseContext = databaseContext;
            this.fileDownloadTaskStepAdvancerFactory = fileDownloadTaskStepAdvancerFactory;
            this.fileDownloadTaskStepProcessFactory = fileDownloadTaskStepProcessFactory;
        }

        public async Task UpdateFromHattrickAsync(
            DownloadSettings settings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            bool isDownloading = true;

            bool isCanceled = false;

            bool hasErrors = false;

            List<IFileDownloadTask> downloadTasks = new List<IFileDownloadTask>
            {
                new XmlFileDownloadTask(XmlFileType.CheckToken, null, null)
            };

            try
            {
                for (int i = 0; i < downloadTasks.Count; i++)
                {
                    var currentTask = downloadTasks[i];

                    // Remarks:
                    // We advance XmlFile tasks up to Processed in order to download all required images to execute the Persister.
                    while ((currentTask.Type == DownloadTaskType.ImageFile &&
                            currentTask.Status != DownloadTaskStatus.Finished) ||
                           (currentTask.Type == DownloadTaskType.XmlFile &&
                            currentTask.Status != DownloadTaskStatus.Processed))
                    {
                        var step = this.fileDownloadTaskStepProcessFactory.GetDownloadTaskStepProcess(currentTask);

                        await step.ExecuteAsync(
                            currentTask,
                            downloadTasks,
                            settings,
                            progress,
                            cancellationToken);

                        ReportProgress(downloadTasks, currentTask, isDownloading, progress);

                        this.fileDownloadTaskStepAdvancerFactory.GetAdvancer(currentTask)
                            .AdvanceTaskStatus(currentTask);
                    }
                }

                // Persist tasks.
                for (int i = 0; i < downloadTasks.Count; i++)
                {
                    var currentTask = downloadTasks[i];

                    while (currentTask.Status != DownloadTaskStatus.Finished)
                    {
                        var step = this.fileDownloadTaskStepProcessFactory.GetDownloadTaskStepProcess(currentTask);

                        await step.ExecuteAsync(
                            currentTask,
                            downloadTasks,
                            settings,
                            progress,
                            cancellationToken);

                        ReportProgress(downloadTasks, currentTask, isDownloading, progress);

                        this.fileDownloadTaskStepAdvancerFactory.GetAdvancer(currentTask)
                            .AdvanceTaskStatus(currentTask);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                isCanceled = true;

                _ = downloadTasks.Where(x => x.Status != DownloadTaskStatus.Finished)
                    .Select(x => { x.Status = DownloadTaskStatus.Canceled; return x; })
                    .ToList();
            }
            catch
            {
                hasErrors = true;

                _ = downloadTasks.Where(x => x.Status != DownloadTaskStatus.Error
                                          && x.Status != DownloadTaskStatus.Finished)
                    .Select(x => { x.Status = DownloadTaskStatus.Canceled; return x; })
                    .ToList();
            }
            finally
            {
                if (hasErrors || isCanceled)
                {
                    this.databaseContext.Cancel();
                }

                isDownloading = false;

                await this.databaseContext.EndTransactionAsync();

                ReportProgress(downloadTasks, downloadTasks.Last(), isDownloading, progress);
            }
        }

        private static void ReportProgress(List<IFileDownloadTask> tasks, IFileDownloadTask currentTask, bool isDownloading, IProgress<ProcessReport> progress)
        {
            int taskCount = tasks.Sum(x => x.Type == DownloadTaskType.ImageFile ? 1 : 4);

            int completedTaskCount = tasks.Sum(x => x.Type == DownloadTaskType.ImageFile
                                                            ? x.Status == DownloadTaskStatus.Finished ? 1 : 0
                                                            : x.Status == DownloadTaskStatus.Error || x.Status == DownloadTaskStatus.Canceled ? 0 : (int)x.Status);

            var rows = tasks.Select(x => new TaskRow(
                x.Title,
                x.Type,
                x.Status))
                .ToList();

            progress.Report(
                new ProcessReport(
                    rows,
                    rows.ElementAt(tasks.IndexOf(currentTask)),
                    isDownloading,
                    taskCount,
                    completedTaskCount));
        }
    }
}