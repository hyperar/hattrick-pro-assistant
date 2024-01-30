namespace Hyperar.HPA.Application.Hattrick.StaffList
{
    using System;
    using Common.Enums;

    public class Staff
    {
        public uint Cost { get; set; }

        public DateTime HiredDate { get; set; }

        public uint HofPlayerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint StaffId { get; set; }

        public uint StaffLevel { get; set; }

        public StaffType StaffType { get; set; }
    }
}