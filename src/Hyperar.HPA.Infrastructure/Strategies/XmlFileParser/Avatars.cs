namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Avatars;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class Avatars : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string layerNodeName = "Layer";

        private const string playerNodeName = "Player";

        private const string xAttributeName = "x";

        private const string yAttributeName = "y";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<Avatar> ParseAvatarNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new Avatar
            {
                BackgroundImage = await reader.ReadElementContentAsStringAsync()
            };

            while (reader.Name == layerNodeName)
            {
                result.Layers.Add(
                    await ParseLayerNodeAsync(reader));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Layer> ParseLayerNodeAsync(XmlReader reader)
        {
            var result = new Layer
            {
                X = uint.Parse(reader.GetAttribute(xAttributeName) ?? "0"),
                Y = uint.Parse(reader.GetAttribute(yAttributeName) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Image = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsUintAsync(),
                Avatar = await ParseAvatarNodeAsync(reader),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Player>> ParsePlayersNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new List<Player>();

            while (reader.Name == playerNodeName)
            {
                result.Add(await ParsePlayerNodeAsync(reader));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Team
            {
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                Players = await ParsePlayersNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}