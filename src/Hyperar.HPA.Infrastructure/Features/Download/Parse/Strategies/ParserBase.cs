namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading.Tasks;
    using System.Xml;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick;

    public abstract class ParserBase
    {
        protected static async Task<IdName> ParseIdNameNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            IdName result = new IdName
            {
                Id = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        protected static async Task<IdName?> ParseNullableIdNameNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            IdName? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                result = new IdName
                {
                    Id = await reader.ReadXmlValueAsLongAsync(),
                    Name = await reader.ReadElementContentAsStringAsync()
                };
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}