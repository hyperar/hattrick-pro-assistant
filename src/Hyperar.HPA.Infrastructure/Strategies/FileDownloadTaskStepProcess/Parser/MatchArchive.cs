namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.MatchArchive;

    public class MatchArchive : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public MatchArchive(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.IsYouth = await reader.ReadXmlValueAsBoolAsync();
            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<AwayTeam> ParseAwayTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            AwayTeam result = new AwayTeam
            {
                AwayTeamId = await reader.ReadXmlValueAsLongAsync(),
                AwayTeamName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<HomeTeam> ParseHomeTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            HomeTeam result = new HomeTeam
            {
                HomeTeamId = await reader.ReadXmlValueAsLongAsync(),
                HomeTeamName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Match> ParseMatchNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Match result = new Match
            {
                MatchId = await reader.ReadXmlValueAsLongAsync(),
                HomeTeam = await ParseHomeTeamNodeAsync(reader),
                AwayTeam = await ParseAwayTeamNodeAsync(reader),
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

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
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