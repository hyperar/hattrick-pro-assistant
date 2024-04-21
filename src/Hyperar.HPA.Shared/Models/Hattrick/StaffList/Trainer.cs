namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using System;

    public class Trainer
    {
        public Trainer()
        {
            this.Name = string.Empty;
        }

        public byte Age { get; set; }

        public byte AgeDays { get; set; }

        public DateTime ContractDate { get; set; }

        public long Cost { get; set; }

        public long CountryId { get; set; }

        public byte Leadership { get; set; }

        public string Name { get; set; }

        public long TrainerId { get; set; }

        public byte TrainerSkillLevel { get; set; }

        public byte TrainerStatus { get; set; }

        public byte TrainerType { get; set; }
    }
}