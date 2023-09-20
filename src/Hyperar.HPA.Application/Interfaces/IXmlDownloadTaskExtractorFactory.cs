namespace Hyperar.HPA.Application.Interfaces
{
    using Hyperar.HPA.Common.Enums;

    public interface IXmlDownloadTaskExtractorFactory
    {
        IXmlDownloadTaskExtractorStrategy CreateDownloadTaskExtractor(XmlFileType fileType);
    }
}