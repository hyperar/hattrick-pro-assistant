namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.HallOfFamePlayers;
    using Shared.Models.Hattrick.Interfaces;

    public class HallOfFamePlayers : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public HallOfFamePlayers(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.PlayerList = await ParsePlayerListNodeAsync(reader);

            return result;
        }

        private static async Task<List<Player>> ParsePlayerListNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            List<Player> result = new List<Player>();

            while (reader.CheckNode(NodeName.Player))
            {
                result.Add(
                    await ParsePlayerNodeAsync(
                        reader));
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadXmlValueAsNullableStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsByteAsync(),
                NextBirthday = await reader.ReadXmlValueAsDateTimeAsync(),
                CountryId = await reader.ReadXmlValueAsLongAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                ExpertType = await reader.ReadXmlValueAsByteAsync(),
                HofDate = await reader.ReadXmlValueAsDateTimeAsync(),
                HofAge = await reader.ReadXmlValueAsShortAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}