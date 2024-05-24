namespace Hyperar.HPA.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using Application.Models;
    using Shared.Models.UI.Download;

    public interface IDownloadViewService
    {
        Task UpdateFromHattrickAsync(
            DownloadSettings settings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken);
    }
}