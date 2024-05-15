namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using System;

    public class Trainer
    {
        public Trainer()
        {
            this.Name = string.Empty;
        }

        public int Age { get; set; }

        public int AgeDays { get; set; }

        public DateTime ContractDate { get; set; }

        public long Cost { get; set; }

        public long CountryId { get; set; }

        public int Leadership { get; set; }

        public string Name { get; set; }

        public long TrainerId { get; set; }

        public int TrainerSkillLevel { get; set; }

        public int TrainerStatus { get; set; }

        public int TrainerType { get; set; }
    }
}