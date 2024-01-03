namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public abstract class XmlFileParserBase : IXmlFileParserStrategy
    {
        public async Task<IXmlFile> ParseAsync(XmlReader reader, IXmlFile result)
        {
            result.Version = await reader.ReadXmlValueAsDecimalAsync();
            result.UserId = await reader.ReadXmlValueAsUintAsync();
            result.FetchedDate = await reader.ReadXmlValueAsDateTimeAsync();

            result = await this.ParseFileTypeSpecificContentAsync(reader, result);

            return result;
        }

        public abstract Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity);
    }
}