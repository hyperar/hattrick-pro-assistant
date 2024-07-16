namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.YouthLeagueDetails;

    public class YouthLeagueDetails : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.YouthLeagueId = await reader.ReadXmlValueAsLongAsync();
            result.YouthLeagueName = await reader.ReadElementContentAsStringAsync();
            result.YouthLeagueType = await reader.ReadXmlValueAsIntAsync();
            result.Season = await reader.ReadXmlValueAsIntAsync();
            result.LastMatchRound = await reader.ReadXmlValueAsIntAsync();
            result.NrOfTeamsInLeague = await reader.ReadXmlValueAsIntAsync();

            while (reader.CheckNode(NodeName.Team))
            {
                result.Teams.Add(
                    await ParseTeamNodeAsync(
                        reader,
                        cancellationToken));
            }
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                Position = await reader.ReadXmlValueAsIntAsync(),
                PositionChange = await reader.ReadXmlValueAsIntAsync(),
                Matches = await reader.ReadXmlValueAsIntAsync(),
                GoalsFor = await reader.ReadXmlValueAsIntAsync(),
                GoalsAgainst = await reader.ReadXmlValueAsIntAsync(),
                Points = await reader.ReadXmlValueAsIntAsync(),
                Won = await reader.ReadXmlValueAsIntAsync(),
                Draws = await reader.ReadXmlValueAsIntAsync(),
                Lost = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}