namespace Hyperar.HPA.Application.Hattrick.ArenaDetails
{
    using System;

    public class ExpandedCapacity
    {
        public bool Available { get; set; }

        public uint? Basic { get; set; }

        public DateTime? ExpansionDate { get; set; }

        public uint? Roof { get; set; }

        public uint? Terraces { get; set; }

        public uint? Total { get; set; }

        public uint? Vip { get; set; }
    }
}