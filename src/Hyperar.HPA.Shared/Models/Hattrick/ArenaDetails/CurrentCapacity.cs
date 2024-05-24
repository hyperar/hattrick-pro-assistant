namespace Hyperar.HPA.Shared.Models.Hattrick.ArenaDetails
{
    using System;

    public class CurrentCapacity
    {
        public bool Available { get; set; }

        public int Basic { get; set; }

        public DateTime? RebuiltDate { get; set; }

        public int Roof { get; set; }

        public int Terraces { get; set; }

        public int Total { get; set; }

        public int Vip { get; set; }
    }
}