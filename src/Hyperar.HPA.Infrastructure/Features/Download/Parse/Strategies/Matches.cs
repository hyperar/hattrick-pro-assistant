namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.Matches;

    public class Matches : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.IsYouth = await reader.ReadXmlValueAsBoolAsync();
            result.Team = await ParseTeamNodeAsync(reader, cancellationToken);
        }

        protected static async Task<IdName> ParseLeagueNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            IdName result = new IdName
            {
                Id = await reader.ReadXmlValueAsLongAsync(),
                Name = reader.CheckNode(NodeName.LeagueName) ? await reader.ReadElementContentAsStringAsync() : string.Empty
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<AwayTeam> ParseAwayTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            AwayTeam result = new AwayTeam
            {
                AwayTeamId = await reader.ReadXmlValueAsLongAsync(),
                AwayTeamName = await reader.ReadElementContentAsStringAsync(),
                AwayTeamShortName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<HomeTeam> ParseHomeTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            HomeTeam result = new HomeTeam
            {
                HomeTeamId = await reader.ReadXmlValueAsLongAsync(),
                HomeTeamName = await reader.ReadElementContentAsStringAsync(),
                HomeTeamShortName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<LeagueLevelUnit> ParseLeagueLevelUnitNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            LeagueLevelUnit result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = await reader.ReadXmlValueAsLongAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevel = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Match> ParseMatchNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Match result = new Match
            {
                MatchId = await reader.ReadXmlValueAsLongAsync(),
                HomeTeam = await ParseHomeTeamNodeAsync(reader, cancellationToken),
                AwayTeam = await ParseAwayTeamNodeAsync(reader, cancellationToken),
                MatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                SourceSystem = await reader.ReadElementContentAsStringAsync(),
                MatchType = await reader.ReadXmlValueAsIntAsync(),
                MatchContextId = await reader.ReadXmlValueAsLongAsync(),
                CupLevel = await reader.ReadXmlValueAsIntAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsIntAsync(),
                HomeGoals = reader.Name.Equals(NodeName.HomeGoals, StringComparison.OrdinalIgnoreCase) ? await reader.ReadXmlValueAsIntAsync() : null,
                AwayGoals = reader.Name.Equals(NodeName.AwayGoals, StringComparison.OrdinalIgnoreCase) ? await reader.ReadXmlValueAsIntAsync() : null,
                OrdersGiven = reader.Name.Equals(NodeName.OrdersGiven, StringComparison.OrdinalIgnoreCase) ? await reader.ReadXmlValueAsBoolAsync() : null,
                Status = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                ShortTeamName = reader.CheckNode(NodeName.ShortTeamName) ? await reader.ReadElementContentAsStringAsync() : null,
                League = await ParseLeagueNodeAsync(reader, cancellationToken),
                LeagueLevelUnit = reader.CheckNode(NodeName.LeagueLevelUnit) ? await ParseLeagueLevelUnitNodeAsync(reader, cancellationToken) : null
            };

            if (reader.CheckNode(NodeName.MatchList))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Match))
                {
                    result.MatchList.Add(await ParseMatchNodeAsync(reader, cancellationToken));
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