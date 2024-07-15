namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.StaffAvatars;

    public class StaffAvatars : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.Trainer = await ParseTrainerNodeAsync(reader, cancellationToken);
            result.StaffMembers = await ParseStaffMembersNodeAsync(reader, cancellationToken);
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

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            List<Staff> result = new List<Staff>();

            while (reader.CheckNode(NodeName.Staff))
            {
                result.Add(await ParseStaffNodeAsync(reader, cancellationToken));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Staff> ParseStaffNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Staff result = new Staff
            {
                StaffId = await reader.ReadXmlValueAsLongAsync(),
                Avatar = await ParseAvatarNodeAsync(reader, cancellationToken),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trainer> ParseTrainerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Trainer result = new Trainer
            {
                TrainerId = await reader.ReadXmlValueAsLongAsync(),
                Avatar = await ParseAvatarNodeAsync(reader, cancellationToken),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}