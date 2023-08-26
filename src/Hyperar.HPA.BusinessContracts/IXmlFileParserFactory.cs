namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Common.Enums;

    public interface IXmlFileParserFactory
    {
        IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType);
    }
}