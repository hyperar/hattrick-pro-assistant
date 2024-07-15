namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.StaffList;

    public class StaffList : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.StaffList = await ParseStaffListNodeAsync(reader, cancellationToken);
        }

        private static async Task<Shared.Models.Hattrick.StaffList.StaffList> ParseStaffListNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Shared.Models.Hattrick.StaffList.StaffList result = new Shared.Models.Hattrick.StaffList.StaffList
            {
                Trainer = await ParseTrainerNodeAsync(reader, cancellationToken),
                StaffMembers = await ParseStaffMembersNodeAsync(reader, cancellationToken),
                TotalStaffMembers = await reader.ReadXmlValueAsLongAsync(),
                TotalCost = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            List<Staff> result = new List<Staff>();

            while (reader.CheckNode(NodeName.Staff))
            {
                result.Add(
                    await ParseStaffNodeAsync(reader, cancellationToken));
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Staff> ParseStaffNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Staff result = new Staff
            {
                Name = await reader.ReadElementContentAsStringAsync(),
                StaffId = await reader.ReadXmlValueAsLongAsync(),
                StaffType = await reader.ReadXmlValueAsIntAsync(),
                StaffLevel = await reader.ReadXmlValueAsIntAsync(),
                HiredDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Cost = await reader.ReadXmlValueAsLongAsync(),
                HofPlayerId = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trainer> ParseTrainerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Trainer result = new Trainer
            {
                TrainerId = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsIntAsync(),
                AgeDays = await reader.ReadXmlValueAsIntAsync(),
                ContractDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Cost = await reader.ReadXmlValueAsLongAsync(),
                CountryId = await reader.ReadXmlValueAsLongAsync(),
                TrainerType = await reader.ReadXmlValueAsIntAsync(),
                Leadership = await reader.ReadXmlValueAsIntAsync(),
                TrainerSkillLevel = await reader.ReadXmlValueAsIntAsync(),
                TrainerStatus = await reader.ReadXmlValueAsIntAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}