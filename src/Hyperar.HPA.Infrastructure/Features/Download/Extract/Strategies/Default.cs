namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Shared.Models.UI.Download;

    public class Default : IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}