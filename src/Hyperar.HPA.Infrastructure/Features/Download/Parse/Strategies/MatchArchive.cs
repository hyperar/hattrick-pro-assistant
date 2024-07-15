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
    using Shared.Models.Hattrick.MatchArchive;

    public class MatchArchive : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.IsYouth = await reader.ReadXmlValueAsBoolAsync();
            result.Team = await ParseTeamNodeAsync(reader, cancellationToken);
        }

        private static async Task<IdName> ParseAwayTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            IdName result = new IdName
            {
                Id = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<IdName> ParseHomeTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            IdName result = new IdName
            {
                Id = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync(),
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
                MatchType = await reader.ReadXmlValueAsIntAsync(),
                MatchContextId = await reader.ReadXmlValueAsLongAsync(),
                SourceSystem = await reader.ReadElementContentAsStringAsync(),
                MatchRuleId = await reader.ReadXmlValueAsIntAsync(),
                CupId = await reader.ReadXmlValueAsLongAsync(),
                CupLevel = await reader.ReadXmlValueAsIntAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsIntAsync(),
                HomeGoals = await reader.ReadXmlValueAsIntAsync(),
                AwayGoals = await reader.ReadXmlValueAsIntAsync()
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