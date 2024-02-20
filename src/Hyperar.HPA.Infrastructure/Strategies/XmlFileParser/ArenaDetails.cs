namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.ArenaDetails;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class ArenaDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.Arena = await ParseArenaNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsUintAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync(),
                ArenaImage = await reader.ReadElementContentAsStringAsync(),
                ArenaFallbackImage = await reader.ReadElementContentAsStringAsync(),
                Team = await ParseTeamNodeAsync(reader),
                League = await ParseLeagueNodeAsync(reader),
                Region = await ParseRegionNodeAsync(reader),
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

            var result = new CurrentCapacity
            {
                Available = reader.GetAttribute(Constants.NodeName.Available) == bool.TrueString
            };

            if (result.Available)
            {
                result.RebuiltDate = await reader.ReadXmlValueAsDateTimeAsync();
            }
            else
            {
                await reader.ReadAsync();
            }

            result.Terraces = await reader.ReadXmlValueAsUintAsync();
            result.Basic = await reader.ReadXmlValueAsUintAsync();
            result.Roof = await reader.ReadXmlValueAsUintAsync();
            result.Vip = await reader.ReadXmlValueAsUintAsync();
            result.Total = await reader.ReadXmlValueAsUintAsync();

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<ExpandedCapacity> ParseExpandedCapacityNodeAsync(XmlReader reader)
        {
            var result = new ExpandedCapacity
            {
                Available = reader.GetAttribute(Constants.NodeName.Available) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            await reader.ReadAsync();

            if (result.Available)
            {
                result.ExpansionDate = await reader.ReadXmlValueAsDateTimeAsync();
                result.Terraces = await reader.ReadXmlValueAsUintAsync();
                result.Basic = await reader.ReadXmlValueAsUintAsync();
                result.Roof = await reader.ReadXmlValueAsUintAsync();
                result.Vip = await reader.ReadXmlValueAsUintAsync();
                result.Total = await reader.ReadXmlValueAsUintAsync();

                // Reads closing element.
                await reader.ReadAsync();
            }

            return result;
        }

        private static async Task<League> ParseLeagueNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new League
            {
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Region> ParseRegionNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new Region
            {
                RegionId = await reader.ReadXmlValueAsUintAsync(),
                RegionName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new Team
            {
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}