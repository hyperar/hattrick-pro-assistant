namespace Hyperar.HPA.Application.Hattrick.Matches
{
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public bool IsYouth { get; set; }

        public Team Team { get; set; } = new Team();
    }
}