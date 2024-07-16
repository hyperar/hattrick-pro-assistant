namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Application.Services;
    using Domain.Interfaces;
    using MediatR;
    using Shared.Enums;
    using Shared.Models.UI.Download;

    public class DownloadViewService : IDownloadViewService
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IDownloadRequestFactory downloadRequestFactory;

        private readonly IMediator mediator;

        private readonly IUserService userService;

        public DownloadViewService(
            IDatabaseContext databaseContext,
            IDownloadRequestFactory downloadRequestFactory,
            IMediator mediator,
            IUserService userService)
        {
            this.databaseContext = databaseContext;
            this.downloadRequestFactory = downloadRequestFactory;
            this.mediator = mediator;
            this.userService = userService;
        }

        public static void ReportProgress(
            DownloadTaskBase currentTask,
            IList<XmlDownloadTask> tasks,
            bool isDownloading,
            IProgress<ProcessReport> progress)
        {
            var allTasks = tasks.SelectMany(x => x.FlattenedTaskList).ToList();

            int taskCount = tasks.Sum(x => x.Count);
            int completedTaskCount = tasks.Sum(x => x.CompletedCount);

            var rows = allTasks.Select(x => new TaskRow(
                x.Title,
                x.Type,
                x.Status))
                .ToList();

            var currentRow = rows.ElementAt(allTasks.IndexOf(currentTask));

            progress.Report(
                new ProcessReport(
                    rows,
                    currentRow,
                    isDownloading,
                    taskCount,
                    completedTaskCount));
        }

        public async Task UpdateFromHattrickAsync(
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            bool isCanceled = false;

            bool hasErrors = false;

            List<XmlDownloadTask> downloadTasks = new List<XmlDownloadTask>
            {
                new XmlDownloadTask(XmlFileType.CheckToken)
            };

            try
            {
                await this.databaseContext.BeginTransactionAsync();

                while (downloadTasks.Sum(x => x.Count) > downloadTasks.Sum(x => x.CompletedCount))
                {
                    var request = this.downloadRequestFactory.Create(downloadTasks, downloadSettings, progress);

                    await this.mediator.Send(request, cancellationToken);
                }

                await this.userService.UpdateUserLastDownloadDate();
            }
            catch (OperationCanceledException)
            {
                isCanceled = true;

                _ = downloadTasks.Where(x => x.Status != DownloadTaskStatus.Finished)
                    .Select(x => { x.Status = DownloadTaskStatus.Canceled; return x; })
                    .ToList();
            }
            catch (Exception ex)
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

                await this.databaseContext.EndTransactionAsync();

                ReportProgress(
                    downloadTasks.Last(),
                    downloadTasks,
                    false,
                    progress);
            }
        }
    }
}