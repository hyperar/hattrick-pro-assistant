namespace Hyperar.HPA.Shared.Models.Hattrick.StaffAvatars
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.StaffMembers = new List<Staff>();
            this.Trainer = new Trainer();
        }

        public List<Staff> StaffMembers { get; set; }

        public Trainer Trainer { get; set; }
    }
}