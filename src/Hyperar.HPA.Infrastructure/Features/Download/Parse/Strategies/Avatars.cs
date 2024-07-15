namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Avatars;
    using Shared.Models.Hattrick.Interfaces;

    public class Avatars : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.Team = await ParseTeamNodeAsync(reader, cancellationToken);
        }

        private static async Task<Avatar> ParseAvatarNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Avatar result = new Avatar
            {
                BackgroundImage = await reader.ReadElementContentAsStringAsync()
            };

            while (reader.CheckNode(NodeName.Layer))
            {
                result.Layers.Add(
                    await ParseLayerNodeAsync(reader, cancellationToken));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Layer> ParseLayerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Layer result = new Layer
            {
                X = byte.Parse(reader.GetAttribute(NodeName.X) ?? "0"),
                Y = byte.Parse(reader.GetAttribute(NodeName.Y) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Image = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                Avatar = await ParseAvatarNodeAsync(reader, cancellationToken),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Player>> ParsePlayersNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            List<Player> result = new List<Player>();

            while (reader.CheckNode(NodeName.Player))
            {
                result.Add(await ParsePlayerNodeAsync(reader, cancellationToken));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                Players = await ParsePlayersNodeAsync(reader, cancellationToken)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}