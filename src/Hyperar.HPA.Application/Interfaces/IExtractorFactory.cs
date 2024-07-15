namespace Hyperar.HPA.Application.Interfaces
{
    public interface IExtractorFactory
    {
        IExtractorStrategy GetExtractor(XmlDownloadTask task);
    }
}