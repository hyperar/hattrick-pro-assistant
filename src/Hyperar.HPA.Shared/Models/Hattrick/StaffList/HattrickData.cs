namespace Hyperar.HPA.Shared.Models.Hattrick.StaffList
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public StaffList StaffList { get; set; } = new StaffList();
    }
}