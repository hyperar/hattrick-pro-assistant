namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Trainer : HattrickEntityBase, IHattrickEntity
    {
        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public byte[] AvatarBytes { get; set; } = Array.Empty<byte>();

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public DateTime HiredOn { get; set; }

        public SkillLevel Leadership { get; set; }

        public int Level { get; set; }

        public string Name { get; set; } = string.Empty;

        public long Salary { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public TrainerType Type { get; set; }
    }
}