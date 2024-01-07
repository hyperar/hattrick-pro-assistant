namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.Matches;
    using Application.Interfaces;
    using Common.Enums;
    using Common.ExtensionMethods;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class Matches : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string awayGoalsNodeName = "AwayGoals";

        private const string homeGoalsNodeName = "HomeGoals";

        private const string matchListNodeName = "MatchList";

        private const string matchNodeName = "Match";

        private const string ordersGivenNodeName = "OrdersGiven";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.IsYouth = await reader.ReadXmlValueAsBoolAsync();
            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<AwayTeam> ParseAwayTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new AwayTeam
            {
                AwayTeamId = await reader.ReadXmlValueAsUintAsync(),
                AwayTeamName = await reader.ReadElementContentAsStringAsync(),
                AwayTeamShortName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<HomeTeam> ParseHomeTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new HomeTeam
            {
                HomeTeamId = await reader.ReadXmlValueAsUintAsync(),
                HomeTeamName = await reader.ReadElementContentAsStringAsync(),
                HomeTeamShortName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<LeagueLevelUnit> ParseLeagueLevelUnitNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevel = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

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

        private static async Task<Match> ParseMatchNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new Match
            {
                MatchId = await reader.ReadXmlValueAsUintAsync(),
                HomeTeam = await ParseHomeTeamNodeAsync(reader),
                AwayTeam = await ParseAwayTeamNodeAsync(reader),
                MatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                SourceSystem = await reader.ReadElementContentAsStringAsync(),
                MatchType = (MatchType)await reader.ReadXmlValueAsUintAsync(),
                MatchContextId = await reader.ReadXmlValueAsUintAsync(),
                CupLevel = await reader.ReadXmlValueAsUintAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsUintAsync(),
                HomeGoals = reader.Name.Equals(homeGoalsNodeName, StringComparison.OrdinalIgnoreCase) ? await reader.ReadXmlValueAsUintAsync() : null,
                AwayGoals = reader.Name.Equals(awayGoalsNodeName, StringComparison.OrdinalIgnoreCase) ? await reader.ReadXmlValueAsUintAsync() : null,
                OrdersGiven = reader.Name.Equals(ordersGivenNodeName, StringComparison.OrdinalIgnoreCase) ? await reader.ReadXmlValueAsBoolAsync() : null,
                Status = (await reader.ReadElementContentAsStringAsync()).ToMatchStatus()
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
                TeamName = await reader.ReadElementContentAsStringAsync(),
                ShortTeamName = await reader.ReadElementContentAsStringAsync(),
                League = await ParseLeagueNodeAsync(reader),
                LeagueLevelUnit = await ParseLeagueLevelUnitNodeAsync(reader)
            };

            if (reader.Name == matchListNodeName)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.Name == matchNodeName)
                {
                    result.MatchList.Add(await ParseMatchNodeAsync(reader));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}