namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public abstract class XmlFileParserBase : IXmlFileParserStrategy
    {
        public void Parse(XmlReader reader, ref IXmlFile result)
        {
            result.Version = reader.ReadXmlValueAsDecimal();
            result.UserId = reader.ReadXmlValueAsUint();
            result.FetchedDate = reader.ReadXmlValueAsDateTime();

            this.ParseFileTypeSpecificContent(reader, ref result);
        }

        public abstract void ParseFileTypeSpecificContent(XmlReader reader, ref IXmlFile entity);
    }
}