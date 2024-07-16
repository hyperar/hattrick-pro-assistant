namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;

    public class Default : IPersisterStrategy
    {
        public Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}