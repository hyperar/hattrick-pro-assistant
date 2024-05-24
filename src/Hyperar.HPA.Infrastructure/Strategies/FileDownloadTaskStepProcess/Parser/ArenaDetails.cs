namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.ArenaDetails;
    using Shared.Models.Hattrick.Interfaces;

    public class ArenaDetails : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public ArenaDetails(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Arena = await ParseArenaNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Arena result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsLongAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync(),
                ArenaImage = await reader.ReadElementContentAsStringAsync(),
                ArenaFallbackImage = await reader.ReadElementContentAsStringAsync(),
                Team = await ParseIdNameNodeAsync(reader),
                League = await ParseIdNameNodeAsync(reader),
                Region = await ParseIdNameNodeAsync(reader),
                CurrentCapacity = await ParseCurrentCapacityNodeAsync(reader),
                ExpandedCapacity = await ParseExpandedCapacityNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<CurrentCapacity> ParseCurrentCapacityNodeAsync(XmlReader reader)
        {
            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            await reader.ReadAsync();

            CurrentCapacity result = new CurrentCapacity
            {
                Available = reader.GetAttribute(NodeName.Available) == bool.TrueString
            };

            if (result.Available)
            {
                result.RebuiltDate = await reader.ReadXmlValueAsDateTimeAsync();
            }
            else
            {
                await reader.ReadAsync();
            }

            result.Terraces = await reader.ReadXmlValueAsIntAsync();
            result.Basic = await reader.ReadXmlValueAsIntAsync();
            result.Roof = await reader.ReadXmlValueAsIntAsync();
            result.Vip = await reader.ReadXmlValueAsIntAsync();
            result.Total = await reader.ReadXmlValueAsIntAsync();

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<ExpandedCapacity> ParseExpandedCapacityNodeAsync(XmlReader reader)
        {
            ExpandedCapacity result = new ExpandedCapacity
            {
                Available = reader.GetAttribute(NodeName.Available) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            await reader.ReadAsync();

            if (result.Available)
            {
                result.ExpansionDate = await reader.ReadXmlValueAsDateTimeAsync();
                result.Terraces = await reader.ReadXmlValueAsIntAsync();
                result.Basic = await reader.ReadXmlValueAsIntAsync();
                result.Roof = await reader.ReadXmlValueAsIntAsync();
                result.Vip = await reader.ReadXmlValueAsIntAsync();
                result.Total = await reader.ReadXmlValueAsIntAsync();

                // Reads closing element.
                await reader.ReadAsync();
            }

            return result;
        }
    }
}