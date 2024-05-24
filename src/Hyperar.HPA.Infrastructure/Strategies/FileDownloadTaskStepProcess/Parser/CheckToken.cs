namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using ExtensionMethods;
    using Shared.Models.Hattrick.CheckToken;
    using Shared.Models.Hattrick.Interfaces;

    public class CheckToken : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public CheckToken(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Token = await reader.ReadElementContentAsStringAsync();
            result.Created = await reader.ReadXmlValueAsDateTimeAsync();
            result.User = await reader.ReadXmlValueAsLongAsync();
            result.Expires = await reader.ReadXmlValueAsDateTimeAsync();

            // TODO: Generate a token with scopes to test this.
            await reader.ReadAsync();

            return result;
        }
    }
}