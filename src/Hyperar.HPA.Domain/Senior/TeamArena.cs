namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;

    public class TeamArena : HattrickEntityBase, IHattrickEntity
    {
        public uint BasicSeatCapacity { get; set; }

        public DateTime BuiltOn { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint RoofSeatCapacity { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public uint TeamHattrickId { get; set; }

        public uint TerracesCapacity { get; set; }

        public uint TotalCapacity { get; set; }

        public uint VipLoungeCapacity { get; set; }
    }
}