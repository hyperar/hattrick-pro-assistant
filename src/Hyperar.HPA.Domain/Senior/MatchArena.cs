namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;

    public class MatchArena : EntityBase, IEntity
    {
        public uint? Attendance { get; set; }

        public uint? BasicSeatsSold { get; set; }

        public uint HattrickId { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public uint MatchHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint? RoofSeatsSold { get; set; }

        public uint? TerracesSold { get; set; }

        public uint? VipSeatsSold { get; set; }
    }
}