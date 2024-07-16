namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class StaffMember : HattrickEntityBase, IHattrickEntity
    {
        public byte[] AvatarBytes { get; set; } = Array.Empty<byte>();

        public DateTime HiredOn { get; set; }

        public int Level { get; set; }

        public string Name { get; set; } = string.Empty;

        public long Salary { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public StaffType Type { get; set; }
    }
}