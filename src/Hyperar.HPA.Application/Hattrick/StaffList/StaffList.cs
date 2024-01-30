namespace Hyperar.HPA.Application.Hattrick.StaffList
{
    using System.Collections.Generic;

    public class StaffList
    {
        public List<Staff> StaffMembers { get; set; } = new List<Staff>();

        public uint TotalCost { get; set; }

        public uint TotalStaffMembers { get; set; }

        public Trainer Trainer { get; set; } = new Trainer();
    }
}