namespace Hyperar.HPA.Application.Interfaces
{
    using System.Xml;
    using Application.Hattrick.Interfaces;

    public interface IXmlFileParserStrategy
    {
        Task<IXmlFile> ParseAsync(XmlReader reader, IXmlFile result);
    }
}