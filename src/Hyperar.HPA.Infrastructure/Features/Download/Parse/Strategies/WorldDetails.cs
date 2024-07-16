namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.WorldDetails;

    public class WorldDetails : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            if (file is HattrickData result)
            {
                if (reader.CheckNode(NodeName.LeagueList))
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(NodeName.League))
                    {
                        result.LeagueList.Add(await ParseLeagueNodeAsync(reader, cancellationToken));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }
            }
        }

        private static async Task<Country> ParseCountryNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Country result = new Country
            {
                Available = reader.GetAttribute(NodeName.Available) == bool.TrueString
            };

            // Reads opening element. This could be both opening and closing element if Available attribute is false.
            await reader.ReadAsync();

            if (result.Available)
            {
                result.CountryId = result.Available ? await reader.ReadXmlValueAsLongAsync() : null;
                result.CountryName = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.CurrencyName = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.CurrencyRate = result.Available ? await reader.ReadXmlValueAsDecimalAsync() : null;
                result.CountryCode = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.DateFormat = result.Available ? await reader.ReadElementContentAsStringAsync() : null;
                result.TimeFormat = result.Available ? await reader.ReadElementContentAsStringAsync() : null;

                if (reader.CheckNode(NodeName.RegionList))
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(NodeName.Region))
                    {
                        result.RegionList.Add(await ParseIdNameNodeAsync(reader, cancellationToken));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            return result;
        }

        private static async Task<Cup> ParseCupNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Cup result = new Cup
            {
                CupId = await reader.ReadXmlValueAsLongAsync(),
                CupName = await reader.ReadElementContentAsStringAsync(),
                CupLeagueLevel = await reader.ReadXmlValueAsIntAsync(),
                CupLevel = await reader.ReadXmlValueAsIntAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsIntAsync(),
                MatchRound = await reader.ReadXmlValueAsIntAsync(),
                MatchRoundsLeft = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<League> ParseLeagueNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            League result = new League
            {
                LeagueId = await reader.ReadXmlValueAsLongAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                Season = await reader.ReadXmlValueAsIntAsync(),
                SeasonOffset = await reader.ReadXmlValueAsIntAsync(),
                MatchRound = await reader.ReadXmlValueAsIntAsync(),
                ShortName = await reader.ReadElementContentAsStringAsync(),
                Continent = await reader.ReadElementContentAsStringAsync(),
                ZoneName = await reader.ReadElementContentAsStringAsync(),
                EnglishName = await reader.ReadElementContentAsStringAsync(),
                LanguageId = await reader.ReadXmlValueAsLongAsync(),
                LanguageName = await reader.ReadElementContentAsStringAsync(),
                Country = await ParseCountryNodeAsync(reader, cancellationToken),
            };

            if (reader.CheckNode(NodeName.Cups))
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(NodeName.Cup))
                    {
                        result.Cups.Add(await ParseCupNodeAsync(reader, cancellationToken));
                    }
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            result.NationalTeamId = await reader.ReadXmlValueAsNullableLongAsync(0);
            result.U20TeamId = await reader.ReadXmlValueAsNullableLongAsync(0);
            result.ActiveTeams = await reader.ReadXmlValueAsIntAsync();
            result.ActiveUsers = await reader.ReadXmlValueAsIntAsync();
            result.WaitingUsers = await reader.ReadXmlValueAsIntAsync();
            result.TrainingDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.EconomyDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.CupMatchDate = await reader.ReadXmlValueAsNullableDateTimeAsync();
            result.SeriesMatchDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence1 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence2 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence3 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence5 = await reader.ReadXmlValueAsDateTimeAsync();
            result.Sequence7 = await reader.ReadXmlValueAsDateTimeAsync();
            result.NumberOfLevels = await reader.ReadXmlValueAsIntAsync();

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}