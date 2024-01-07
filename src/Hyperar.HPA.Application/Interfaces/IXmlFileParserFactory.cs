namespace Hyperar.HPA.Application.Interfaces
{
    using Common.Enums;

    public interface IXmlFileParserFactory
    {
        IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType);
    }
}