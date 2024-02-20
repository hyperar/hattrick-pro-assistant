namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.StaffList;
    using Application.Interfaces;
    using Common.Enums;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class StaffList : XmlFileParserBase, IXmlFileParserStrategy
    {
        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.StaffList = await ParseStaffListNodeAsync(reader);

            return result;
        }

        private static async Task<Application.Hattrick.StaffList.StaffList> ParseStaffListNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Application.Hattrick.StaffList.StaffList
            {
                Trainer = await ParseTrainerNodeAsync(reader),
                StaffMembers = await ParseStaffMembersNodeAsync(reader),
                TotalStaffMembers = await reader.ReadXmlValueAsUintAsync(),
                TotalCost = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Staff>> ParseStaffMembersNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new List<Staff>();

            while (reader.CheckNode(Constants.NodeName.Staff))
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

            var result = new Staff
            {
                Name = await reader.ReadElementContentAsStringAsync(),
                StaffId = await reader.ReadXmlValueAsUintAsync(),
                StaffType = (StaffType)await reader.ReadXmlValueAsByteAsync(),
                StaffLevel = await reader.ReadXmlValueAsUintAsync(),
                HiredDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Cost = await reader.ReadXmlValueAsUintAsync(),
                HofPlayerId = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trainer> ParseTrainerNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Trainer
            {
                TrainerId = await reader.ReadXmlValueAsUintAsync(),
                Name = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsUintAsync(),
                AgeDays = await reader.ReadXmlValueAsUintAsync(),
                ContractDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Cost = await reader.ReadXmlValueAsUintAsync(),
                CountryId = await reader.ReadXmlValueAsUintAsync(),
                TrainerType = (TrainerType)await reader.ReadXmlValueAsByteAsync(),
                Leadership = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                TrainerSkillLevel = await reader.ReadXmlValueAsUintAsync(),
                TrainerStatus = (TrainerStatus)await reader.ReadXmlValueAsByteAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}