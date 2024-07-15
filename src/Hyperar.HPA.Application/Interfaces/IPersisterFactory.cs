namespace Hyperar.HPA.Application.Interfaces
{
    public interface IPersisterFactory
    {
        IPersisterStrategy GetPersister(XmlDownloadTask task);
    }
}