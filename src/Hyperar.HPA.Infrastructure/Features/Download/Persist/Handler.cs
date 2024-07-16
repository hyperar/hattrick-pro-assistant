namespace Hyperar.HPA.Infrastructure.Features.Download.Persist
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Features;
    using Application.Interfaces;
    using Domain.Interfaces;
    using MediatR;
    using Shared.Enums;

    public class Handler : IRequestHandler<PersistRequest>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IPersisterFactory persisterFactory;

        public Handler(IDatabaseContext databaseContext,
            IPersisterFactory persisterFactory)
        {
            this.databaseContext = databaseContext;
            this.persisterFactory = persisterFactory;
        }

        public async Task Handle(PersistRequest request, CancellationToken cancellationToken)
        {
            if (request.Task is XmlDownloadTask xmlDownloadTask)
            {
                request.Task.Status = DownloadTaskStatus.Persisting;

                Services.DownloadViewService.ReportProgress(
                    request.Task,
                    request.TaskList,
                    true,
                    request.Progress);

                var persister = this.persisterFactory.GetPersister(xmlDownloadTask);

                await persister.PersistAsync(xmlDownloadTask, cancellationToken);

                await this.databaseContext.SaveAsync();

                request.Task.Status = DownloadTaskStatus.Finished;

                Services.DownloadViewService.ReportProgress(
                    request.Task,
                    request.TaskList,
                    true,
                    request.Progress);
            }
            else
            {
                throw new ArgumentException(nameof(request));
            }
        }
    }
}