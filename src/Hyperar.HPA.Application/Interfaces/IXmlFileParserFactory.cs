namespace Hyperar.HPA.Application.Interfaces
{
    using Hyperar.HPA.Common.Enums;

    public interface IXmlFileParserFactory
    {
        IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType);
    }
}