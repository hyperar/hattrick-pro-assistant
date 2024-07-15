namespace Hyperar.HPA.Application.Interfaces
{
    public interface IPersisterStrategy
    {
        Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken);
    }
}