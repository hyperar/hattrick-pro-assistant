namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.StaffAvatars;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class StaffAvatars : XmlFileParserBase, IXmlFileParserStrategy
    {
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

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            List<Staff> result = new List<Staff>();

            while (reader.CheckNode(Constants.NodeName.Staff))
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
                StaffId = await reader.ReadXmlValueAsUintAsync(),
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
                TrainerId = await reader.ReadXmlValueAsUintAsync(),
                Avatar = await ParseAvatarNodeAsync(reader),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}