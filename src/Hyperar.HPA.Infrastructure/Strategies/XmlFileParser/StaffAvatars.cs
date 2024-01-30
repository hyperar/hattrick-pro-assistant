namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.StaffAvatars;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class StaffAvatars : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string layerNodeName = "Layer";

        private const string staffNodeName = "Staff";

        private const string xAttributeName = "x";

        private const string yAttributeName = "y";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.Trainer = await ParseTrainerNodeAsync(reader);
            result.StaffMembers = await ParseStaffMembersNodeAsync(reader);

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

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new List<Staff>();

            while (reader.Name == staffNodeName)
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

            var result = new Staff
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

            var result = new Trainer
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