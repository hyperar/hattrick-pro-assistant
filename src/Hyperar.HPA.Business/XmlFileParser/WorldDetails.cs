namespace Hyperar.HPA.Business.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Domain.Hattrick;
    using Hyperar.HPA.Domain.Hattrick.WorldDetails;

    public class WorldDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string regionListNodeName = "RegionList";

        private const string cupsNodeName = "Cups";

        private const string availableAttributeName = "Available";

        private const string leagueListNodeName = "LeagueList";

        private const string leagueNodeName = "League";

        private const string regionNodeName = "Region";

        private const string cupNodeName = "Cup";

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref XmlFileBase entity)
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

        private League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            League result = new League
            {
                LeagueId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueName = reader.ReadElementContentAsString(),
                Season = uint.Parse(reader.ReadElementContentAsString()),
                SeasonOffset = int.Parse(reader.ReadElementContentAsString()),
                MatchRound = uint.Parse(reader.ReadElementContentAsString()),
                ShortName = reader.ReadElementContentAsString(),
                Continent = reader.ReadElementContentAsString(),
                ZoneName = reader.ReadElementContentAsString(),
                EnglishName = reader.ReadElementContentAsString(),
                LanguageId = uint.Parse(reader.ReadElementContentAsString()),
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

            result.NationalTeamId = uint.Parse(reader.ReadElementContentAsString());
            result.U20TeamId = uint.Parse(reader.ReadElementContentAsString());
            result.ActiveTeams = uint.Parse(reader.ReadElementContentAsString());
            result.ActiveUsers = uint.Parse(reader.ReadElementContentAsString());
            result.WaitingUsers = uint.Parse(reader.ReadElementContentAsString());
            result.TrainingDate = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.EconomyDate = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.CupMatchDate = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.SeriesMatchDate = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.Sequence1 = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.Sequence2 = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.Sequence3 = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.Sequence5 = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.Sequence7 = this.ParseDateTimeValue(reader.ReadElementContentAsString());
            result.NumberOfLevels = uint.Parse(reader.ReadElementContentAsString());

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Cup ParseCupNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Cup result = new Cup
            {
                CupId = uint.Parse(reader.ReadElementContentAsString()),
                CupName = reader.ReadElementContentAsString(),
                CupLeagueLevel = uint.Parse(reader.ReadElementContentAsString()),
                CupLevel = uint.Parse(reader.ReadElementContentAsString()),
                CupLevelIndex = uint.Parse(reader.ReadElementContentAsString()),
                MatchRound = uint.Parse(reader.ReadElementContentAsString()),
                MatchRoundsLeft = uint.Parse(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Country ParseCountryNode(XmlReader reader)
        {
            Country result = new Country
            {
                Available = reader.GetAttribute(availableAttributeName) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            reader.Read();

            if (result.Available)
            {
                result.CountryId = result.Available ? uint.Parse(reader.ReadElementContentAsString()) : null;
                result.CountryName = result.Available ? reader.ReadElementContentAsString() : null;
                result.CurrencyName = result.Available ? reader.ReadElementContentAsString() : null;
                result.CurrencyRate = result.Available ? this.ParseDecimalValue(reader.ReadElementContentAsString()) : null;
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

        private Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Region result = new Region
            {
                RegionId = uint.Parse(reader.ReadElementContentAsString()),
                RegionName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }
    }
}
