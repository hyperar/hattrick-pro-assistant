namespace Hyperar.HPA.Application.Hattrick.StaffList
{
    using System;
    using Common.Enums;

    public class Trainer
    {
        public uint Age { get; set; }

        public uint AgeDays { get; set; }

        public DateTime ContractDate { get; set; }

        public uint Cost { get; set; }

        public uint CountryId { get; set; }

        public SkillLevel Leadership { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint TrainerId { get; set; }

        public uint TrainerSkillLevel { get; set; }

        public TrainerStatus TrainerStatus { get; set; }

        public TrainerType TrainerType { get; set; }
    }
}