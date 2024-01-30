namespace Hyperar.HPA.Application.Hattrick.StaffList
{
    using Application.Hattrick;
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public StaffList StaffList { get; set; } = new StaffList();
    }
}