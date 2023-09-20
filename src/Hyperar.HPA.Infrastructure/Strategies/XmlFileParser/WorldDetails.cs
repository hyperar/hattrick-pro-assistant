namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.WorldDetails;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class WorldDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string availableAttributeName = "Available";

        private const string cupNodeName = "Cup";

        private const string cupsNodeName = "Cups";

        private const string leagueListNodeName = "LeagueList";

        private const string leagueNodeName = "League";

        private const string regionListNodeName = "RegionList";

        private const string regionNodeName = "Region";

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref IXmlFile entity)
        {
            var result = (HattrickData)entity;

            if (reader.Name == leagueListNodeName)
            {
                // Reads opening element.
                reader.Read();

                while (reader.Name == leagueNodeName)
                {
                    result.LeagueList.Add(this.ParseLeagueNode(reader));
                }

                // Reads closing element.
                reader.Read();
            }
        }

        private Country ParseCountryNode(XmlReader reader)
        {
            var result = new Country
            {
                Available = reader.GetAttribute(availableAttributeName) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            reader.Read();

            if (result.Available)
            {
                result.CountryId = result.Available ? reader.ReadXmlValueAsUint() : null;
                result.CountryName = result.Available ? reader.ReadElementContentAsString() : null;
                result.CurrencyName = result.Available ? reader.ReadElementContentAsString() : null;
                result.CurrencyRate = result.Available ? reader.ReadXmlValueAsDecimal() : null;
                result.CountryCode = result.Available ? reader.ReadElementContentAsString() : null;
                result.DateFormat = result.Available ? reader.ReadElementContentAsString() : null;
                result.TimeFormat = result.Available ? reader.ReadElementContentAsString() : null;

                if (reader.Name == regionListNodeName)
                {
                    // Reads opening element.
                    reader.Read();

                    while (reader.Name == regionNodeName)
                    {
                        result.RegionList.Add(this.ParseRegionNode(reader));
                    }

                    // Reads closing element.
                    reader.Read();
                }

                // Reads closing element.
                reader.Read();
            }

            return result;
        }

        private Cup ParseCupNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Cup
            {
                CupId = reader.ReadXmlValueAsUint(),
                CupName = reader.ReadElementContentAsString(),
                CupLeagueLevel = reader.ReadXmlValueAsUint(),
                CupLevel = reader.ReadXmlValueAsUint(),
                CupLevelIndex = reader.ReadXmlValueAsUint(),
                MatchRound = reader.ReadXmlValueAsUint(),
                MatchRoundsLeft = reader.ReadXmlValueAsUint()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new League
            {
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString(),
                Season = reader.ReadXmlValueAsUint(),
                SeasonOffset = reader.ReadXmlValueAsInt(),
                MatchRound = reader.ReadXmlValueAsUint(),
                ShortName = reader.ReadElementContentAsString(),
                Continent = reader.ReadElementContentAsString(),
                ZoneName = reader.ReadElementContentAsString(),
                EnglishName = reader.ReadElementContentAsString(),
                LanguageId = reader.ReadXmlValueAsUint(),
                LanguageName = reader.ReadElementContentAsString(),
                Country = this.ParseCountryNode(reader),
            };

            if (reader.Name == cupsNodeName)
            {
                // Reads opening element.
                reader.Read();

                while (reader.Name == cupNodeName)
                {
                    result.Cups.Add(this.ParseCupNode(reader));
                }

                // Reads closing element.
                reader.Read();
            }

            result.NationalTeamId = reader.ReadXmlValueAsUint();
            result.U20TeamId = reader.ReadXmlValueAsUint();
            result.ActiveTeams = reader.ReadXmlValueAsUint();
            result.ActiveUsers = reader.ReadXmlValueAsUint();
            result.WaitingUsers = reader.ReadXmlValueAsUint();
            result.TrainingDate = reader.ReadXmlValueAsDateTime();
            result.EconomyDate = reader.ReadXmlValueAsDateTime();
            result.CupMatchDate = reader.ReadXmlValueAsDateTime();
            result.SeriesMatchDate = reader.ReadXmlValueAsDateTime();
            result.Sequence1 = reader.ReadXmlValueAsDateTime();
            result.Sequence2 = reader.ReadXmlValueAsDateTime();
            result.Sequence3 = reader.ReadXmlValueAsDateTime();
            result.Sequence5 = reader.ReadXmlValueAsDateTime();
            result.Sequence7 = reader.ReadXmlValueAsDateTime();
            result.NumberOfLevels = reader.ReadXmlValueAsUint();

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Region
            {
                RegionId = reader.ReadXmlValueAsUint(),
                RegionName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }
    }
}