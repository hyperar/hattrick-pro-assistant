namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.StaffList;

    public class StaffList : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public StaffList(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.StaffList = await ParseStaffListNodeAsync(reader);

            return result;
        }

        private static async Task<Shared.Models.Hattrick.StaffList.StaffList> ParseStaffListNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Shared.Models.Hattrick.StaffList.StaffList result = new Shared.Models.Hattrick.StaffList.StaffList
            {
                Trainer = await ParseTrainerNodeAsync(reader),
                StaffMembers = await ParseStaffMembersNodeAsync(reader),
                TotalStaffMembers = await reader.ReadXmlValueAsLongAsync(),
                TotalCost = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            List<Staff> result = new List<Staff>();

            while (reader.CheckNode(NodeName.Staff))
            {
                result.Add(
                    await ParseStaffNodeAsync(reader));
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Staff> ParseStaffNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Staff result = new Staff
            {
                Name = await reader.ReadElementContentAsStringAsync(),
                StaffId = await reader.ReadXmlValueAsLongAsync(),
                StaffType = await reader.ReadXmlValueAsByteAsync(),
                StaffLevel = await reader.ReadXmlValueAsByteAsync(),
                HiredDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Cost = await reader.ReadXmlValueAsLongAsync(),
                HofPlayerId = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trainer> ParseTrainerNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Trainer result = new Trainer
            {
                TrainerId = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsByteAsync(),
                AgeDays = await reader.ReadXmlValueAsByteAsync(),
                ContractDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Cost = await reader.ReadXmlValueAsLongAsync(),
                CountryId = await reader.ReadXmlValueAsLongAsync(),
                TrainerType = await reader.ReadXmlValueAsByteAsync(),
                Leadership = await reader.ReadXmlValueAsByteAsync(),
                TrainerSkillLevel = await reader.ReadXmlValueAsByteAsync(),
                TrainerStatus = await reader.ReadXmlValueAsByteAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}