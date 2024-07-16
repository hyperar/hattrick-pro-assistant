namespace Hyperar.HPA.Application.Interfaces
{
    using System.Xml;
    using Shared.Models.Hattrick.Interfaces;

    public interface IParserStrategy
    {
        Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken);
    }
}