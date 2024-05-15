namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.Matches;

    public class Matches : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public Matches(IXmlEntityFactory entityFactory) : base(entityFactory)
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

        private static async Task<LeagueLevelUnit> ParseLeagueLevelUnitNodeAsync(XmlReader reader)
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

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                ShortTeamName = await reader.ReadElementContentAsStringAsync(),
                League = await ParseIdNameNodeAsync(reader),
                LeagueLevelUnit = await ParseLeagueLevelUnitNodeAsync(reader)
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