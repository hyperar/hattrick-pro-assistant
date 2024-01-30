namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.HallOfFamePlayers;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class HallOfFamePlayers : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string playerListNodeName = "PlayerList";

        private const string playerNodeName = "Player";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.PlayerList = await ParsePlayerListNodeAsync(reader);

            return result;
        }

        private static async Task<List<Player>> ParsePlayerListNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new List<Player>();

            while (reader.Name == playerNodeName)
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

            var result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsUintAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadXmlValueAsNullableStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsUintAsync(),
                NextBirthday = await reader.ReadXmlValueAsDateTimeAsync(),
                CountryId = await reader.ReadXmlValueAsUintAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                ExpertType = await reader.ReadXmlValueAsHallOfFameExpertTypeAsync(),
                HofDate = await reader.ReadXmlValueAsDateTimeAsync(),
                HofAge = await reader.ReadXmlValueAsUintAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}