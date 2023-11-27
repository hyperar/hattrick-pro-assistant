namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.ArenaDetails;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class ArenaDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string availableAttributeName = "Available";

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.Arena = ParseArenaNode(reader);
        }

        private static Arena ParseArenaNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Arena
            {
                ArenaId = reader.ReadXmlValueAsUint(),
                ArenaName = reader.ReadElementContentAsString(),
                ArenaImage = reader.ReadElementContentAsString(),
                ArenaFallbackImage = reader.ReadElementContentAsString(),
                Team = ParseTeamNode(reader),
                League = ParseLeagueNode(reader),
                Region = ParseRegionNode(reader),
                CurrentCapacity = ParseCurrentCapacityNode(reader),
                ExpandedCapacity = ParseExpandedCapacityNode(reader)
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static CurrentCapacity ParseCurrentCapacityNode(XmlReader reader)
        {
            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            reader.Read();

            var result = new CurrentCapacity
            {
                Available = reader.GetAttribute(availableAttributeName) == bool.TrueString
            };

            if (result.Available)
            {
                result.RebuiltDate = reader.ReadXmlValueAsDateTime();
            }
            else
            {
                reader.Read();
            }

            result.Terraces = reader.ReadXmlValueAsUint();
            result.Basic = reader.ReadXmlValueAsUint();
            result.Roof = reader.ReadXmlValueAsUint();
            result.Vip = reader.ReadXmlValueAsUint();
            result.Total = reader.ReadXmlValueAsUint();

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static ExpandedCapacity ParseExpandedCapacityNode(XmlReader reader)
        {
            var result = new ExpandedCapacity
            {
                Available = reader.GetAttribute(availableAttributeName) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            reader.Read();

            if (result.Available)
            {
                result.ExpansionDate = reader.ReadXmlValueAsDateTime();
                result.Terraces = reader.ReadXmlValueAsUint();
                result.Basic = reader.ReadXmlValueAsUint();
                result.Roof = reader.ReadXmlValueAsUint();
                result.Vip = reader.ReadXmlValueAsUint();
                result.Total = reader.ReadXmlValueAsUint();

                // Reads closing element.
                reader.Read();
            }

            return result;
        }

        private static League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            var result = new League
            {
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString()
            };

            // Reads closing node.
            reader.Read();

            return result;
        }

        private static Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            var result = new Region
            {
                RegionId = reader.ReadXmlValueAsUint(),
                RegionName = reader.ReadElementContentAsString()
            };

            // Reads closing node.
            reader.Read();

            return result;
        }

        private static Team ParseTeamNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            var result = new Team
            {
                TeamId = reader.ReadXmlValueAsUint(),
                TeamName = reader.ReadElementContentAsString()
            };

            // Reads closing node.
            reader.Read();

            return result;
        }
    }
}