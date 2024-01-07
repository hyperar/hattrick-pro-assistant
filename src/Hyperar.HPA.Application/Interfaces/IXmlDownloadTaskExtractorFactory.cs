namespace Hyperar.HPA.Application.Interfaces
{
    using Common.Enums;

    public interface IXmlDownloadTaskExtractorFactory
    {
        IXmlDownloadTaskExtractorStrategy CreateDownloadTaskExtractor(XmlFileType fileType);
    }
}