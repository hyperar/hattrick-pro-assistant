namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Team = new Team();
        }

        public bool IsYouth { get; set; }

        public Team Team { get; set; }
    }
}