namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Shared.Models.Hattrick.Interfaces;

    public class Default : IParserStrategy
    {
        public Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}