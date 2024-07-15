namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using System;

    public class Staff
    {
        public long Cost { get; set; }

        public DateTime HiredDate { get; set; }

        public long HofPlayerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public long StaffId { get; set; }

        public int StaffLevel { get; set; }

        public int StaffType { get; set; }
    }
}