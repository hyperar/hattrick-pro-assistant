namespace Hyperar.HPA.BusinessContracts
{
    using System.Xml;
    using Hyperar.HPA.Domain.Hattrick;

    public interface IXmlFileParserStrategy
    {
        void Parse(XmlReader reader, ref XmlFileBase result);
    }
}