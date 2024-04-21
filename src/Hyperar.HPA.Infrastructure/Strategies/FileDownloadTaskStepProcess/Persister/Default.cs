namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;

    public class Default : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
        }
    }
}