namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class SeniorTeamArena : HattrickEntityBase, IHattrickEntity
    {
        public uint BasicSeatCapacity { get; set; }

        public DateTime BuiltOn { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint RoofSeatCapacity { get; set; }

        public virtual SeniorTeam SeniorTeam { get; set; } = new SeniorTeam();

        public uint SeniorTeamHattrickId { get; set; }

        public uint TerracesCapacity { get; set; }

        public uint TotalCapacity { get; set; }

        public uint VipLoungeCapacity { get; set; }
    }
}