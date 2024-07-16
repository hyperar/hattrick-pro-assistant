namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using System.Collections.Generic;

    public class StaffList
    {
        public List<Staff> StaffMembers { get; set; } = new List<Staff>();

        public long TotalCost { get; set; }

        public long TotalStaffMembers { get; set; }

        public Trainer Trainer { get; set; } = new Trainer();
    }
}