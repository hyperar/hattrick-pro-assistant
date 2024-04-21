namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using System;

    public class Staff
    {
        public Staff()
        {
            this.Name = string.Empty;
        }

        public long Cost { get; set; }

        public DateTime HiredDate { get; set; }

        public long HofPlayerId { get; set; }

        public string Name { get; set; }

        public long StaffId { get; set; }

        public byte StaffLevel { get; set; }

        public byte StaffType { get; set; }
    }
}