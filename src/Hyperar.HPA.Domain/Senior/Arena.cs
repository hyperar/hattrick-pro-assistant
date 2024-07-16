namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;

    public class Arena : HattrickEntityBase, IHattrickEntity
    {
        public int BasicCapacity { get; set; }

        public int? BasicCapacityChange { get; set; }

        public DateTime? BuiltOn { get; set; }

        public int? CapacityChange { get; set; }

        public DateTime? ConstructionEndsOn { get; set; }

        public int CurrentCapacity { get; set; }

        public byte[] ImageBytes { get; set; } = Array.Empty<byte>();

        public string Name { get; set; } = string.Empty;

        public int RoofCapacity { get; set; }

        public int? RoofCapacityChange { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public int TerracesCapacity { get; set; }

        public int? TerracesCapacityChange { get; set; }

        public int VipCapacity { get; set; }

        public int? VipCapacityChange { get; set; }
    }
}