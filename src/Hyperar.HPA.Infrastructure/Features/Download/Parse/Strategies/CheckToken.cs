namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.CheckToken;
    using Shared.Models.Hattrick.Interfaces;

    public class CheckToken : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.Token = await reader.ReadElementContentAsStringAsync();
            result.Created = await reader.ReadXmlValueAsDateTimeAsync();
            result.User = await reader.ReadXmlValueAsLongAsync();
            result.Expires = await reader.ReadXmlValueAsDateTimeAsync();
            result.ExtendedPermissions = (await reader.ReadElementContentAsStringAsync())
                .Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}