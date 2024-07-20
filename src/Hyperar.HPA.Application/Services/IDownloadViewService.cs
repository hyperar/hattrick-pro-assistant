namespace Hyperar.HPA.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using Shared.Models.UI.Download;

    public interface IDownloadViewService
    {
        Task<bool> UpdateFromHattrickAsync(
            DownloadSettings settings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken);
    }
}