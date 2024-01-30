namespace Hyperar.HPA.Application.Hattrick.StaffAvatars
{
    using Application.Hattrick;
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public List<Staff> StaffMembers { get; set; } = new List<Staff>();

        public Trainer Trainer { get; set; } = new Trainer();
    }
}