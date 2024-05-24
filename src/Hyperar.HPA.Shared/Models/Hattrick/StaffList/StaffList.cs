namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using System.Collections.Generic;

    public class StaffList
    {
        public StaffList()
        {
            this.StaffMembers = new List<Staff>();
            this.Trainer = new Trainer();
        }

        public List<Staff> StaffMembers { get; set; }

        public long TotalCost { get; set; }

        public long TotalStaffMembers { get; set; }

        public Trainer Trainer { get; set; }
    }
}