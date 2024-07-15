namespace Hyperar.HPA.Shared.Models.Hattrick.StaffAvatars
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public List<Staff> StaffMembers { get; set; } = new List<Staff>();

        public Trainer Trainer { get; set; } = new Trainer();
    }
}