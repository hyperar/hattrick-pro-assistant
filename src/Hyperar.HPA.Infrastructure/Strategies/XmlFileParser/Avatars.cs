namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Avatars;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class Avatars : XmlFileParserBase, IXmlFileParserStrategy
    {
        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<Avatar> ParseAvatarNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Avatar result = new Avatar
            {
                BackgroundImage = await reader.ReadElementContentAsStringAsync()
            };

            while (reader.CheckNode(Constants.NodeName.Layer))
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
            Layer result = new Layer
            {
                X = uint.Parse(reader.GetAttribute(Constants.NodeName.X) ?? "0"),
                Y = uint.Parse(reader.GetAttribute(Constants.NodeName.Y) ?? "0"),
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

            Player result = new Player
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

            List<Player> result = new List<Player>();

            while (reader.CheckNode(Constants.NodeName.Player))
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

            Team result = new Team
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