namespace Hyperar.HPA.Application.Interfaces
{
    public interface IParserFactory
    {
        IParserStrategy GetParser(XmlDownloadTask task);
    }
}