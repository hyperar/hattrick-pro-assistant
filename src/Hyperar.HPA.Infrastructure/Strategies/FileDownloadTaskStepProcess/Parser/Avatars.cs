namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Avatars;
    using Shared.Models.Hattrick.Interfaces;

    public class Avatars : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public Avatars(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

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

            while (reader.CheckNode(NodeName.Layer))
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

        private static async Task<Player> ParsePlayerNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
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

            while (reader.CheckNode(NodeName.Player))
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
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                Players = await ParsePlayersNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}