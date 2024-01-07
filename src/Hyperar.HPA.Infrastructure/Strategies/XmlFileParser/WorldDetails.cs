namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.WorldDetails;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class WorldDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string availableAttributeName = "Available";

        private const string cupNodeName = "Cup";

        private const string cupsNodeName = "Cups";

        private const string leagueListNodeName = "LeagueList";

        private const string leagueNodeName = "League";

        private const string regionListNodeName = "RegionList";

        private const string regionNodeName = "Region";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            if (reader.Name == leagueListNodeName)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.Name == leagueNodeName)
                {
                    result.LeagueList.Add(await ParseLeagueNodeAsync(reader));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            return result;
        }

        private static async Task<Country> ParseCountryNodeAsync(XmlReader reader)
        {
            var result = new Country
            {
                Available = reader.GetAttribute(availableAttributeName) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            await reader.ReadAsync();

            if (result.Available)
            {
                result.CountryId = result.Available ? await reader.ReadXmlValueAsUintAsync() : null;
                result.CountryName = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.CurrencyName = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.CurrencyRate = result.Available ? await reader.ReadXmlValueAsDecimalAsync() : null;
                result.CountryCode = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.DateFormat = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.TimeFormat = result.Available ? await reader.ReadElementContentAsStringAsync() : null;

                if (reader.Name == regionListNodeName)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.Name == regionNodeName)
                    {
                        result.RegionList.Add(await ParseRegionNodeAsync(reader));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            return result;
        }

        private static async Task<Cup> ParseCupNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Cup
            {
                CupId = await reader.ReadXmlValueAsUintAsync(),
                CupName = await reader.ReadElementContentAsStringAsync(),
                CupLeagueLevel = await reader.ReadXmlValueAsUintAsync(),
                CupLevel = await reader.ReadXmlValueAsUintAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsUintAsync(),
                MatchRound = await reader.ReadXmlValueAsUintAsync(),
                MatchRoundsLeft = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<League> ParseLeagueNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new League
            {
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                Season = await reader.ReadXmlValueAsUintAsync(),
                SeasonOffset = await reader.ReadXmlValueAsIntAsync(),
                MatchRound = await reader.ReadXmlValueAsUintAsync(),
                ShortName = await reader.ReadElementContentAsStringAsync(),
                Continent = await reader.ReadElementContentAsStringAsync(),
                ZoneName = await reader.ReadElementContentAsStringAsync(),
                EnglishName = await reader.ReadElementContentAsStringAsync(),
                LanguageId = await reader.ReadXmlValueAsUintAsync(),
                LanguageName = await reader.ReadElementContentAsStringAsync(),
                Country = await ParseCountryNodeAsync(reader),
            };

            if (reader.Name == cupsNodeName)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.Name == cupNodeName)
                {
                    result.Cups.Add(await ParseCupNodeAsync(reader));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            result.NationalTeamId = await reader.ReadXmlValueAsUintAsync();
            result.U20TeamId = await reader.ReadXmlValueAsUintAsync();
            result.ActiveTeams = await reader.ReadXmlValueAsUintAsync();
            result.ActiveUsers = await reader.ReadXmlValueAsUintAsync();
            result.WaitingUsers = await reader.ReadXmlValueAsUintAsync();
            result.TrainingDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.EconomyDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.CupMatchDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.SeriesMatchDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence1 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence2 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence3 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence5 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence7 = await reader.ReadXmlValueAsDateTimeAsync();
            result.NumberOfLevels = await reader.ReadXmlValueAsUintAsync();

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Region> ParseRegionNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Region
            {
                RegionId = await reader.ReadXmlValueAsUintAsync(),
                RegionName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}