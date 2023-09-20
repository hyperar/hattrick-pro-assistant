namespace Hyperar.HPA.Application.Interfaces
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    public interface IXmlFileParserStrategy
    {
        void Parse(XmlReader reader, ref IXmlFile result);
    }
}