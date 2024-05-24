namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.StaffAvatars;

    public class StaffAvatars : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public StaffAvatars(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Trainer = await ParseTrainerNodeAsync(reader);
            result.StaffMembers = await ParseStaffMembersNodeAsync(reader);

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

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            List<Staff> result = new List<Staff>();

            while (reader.CheckNode(NodeName.Staff))
            {
                result.Add(await ParseStaffNodeAsync(reader));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Staff> ParseStaffNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Staff result = new Staff
            {
                StaffId = await reader.ReadXmlValueAsLongAsync(),
                Avatar = await ParseAvatarNodeAsync(reader),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trainer> ParseTrainerNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Trainer result = new Trainer
            {
                TrainerId = await reader.ReadXmlValueAsLongAsync(),
                Avatar = await ParseAvatarNodeAsync(reader),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}