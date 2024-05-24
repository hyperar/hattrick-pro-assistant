namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Club;
    using Shared.Models.Hattrick.Interfaces;

    public class Club : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public Club(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<Staff> ParseStaffNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Staff result = new Staff
            {
                AssistantTrainerLevels = await reader.ReadXmlValueAsIntAsync(),
                FinancialDirectorLevels = await reader.ReadXmlValueAsIntAsync(),
                FormCoachLevels = await reader.ReadXmlValueAsIntAsync(),
                MedicLevels = await reader.ReadXmlValueAsIntAsync(),
                SpokespersonLevels = await reader.ReadXmlValueAsIntAsync(),
                SportPsychologistLevels = await reader.ReadXmlValueAsIntAsync(),
                TacticalAssistantLevels = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                Staff = await ParseStaffNodeAsync(reader),
                YouthSquad = await ParseYouthSquadNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<YouthSquad> ParseYouthSquadNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            YouthSquad result = new YouthSquad
            {
                Investment = await reader.ReadXmlValueAsLongAsync(),
                HasPromoted = await reader.ReadXmlValueAsBoolAsync(),
                YouthLevel = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}