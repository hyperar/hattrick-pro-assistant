namespace Hyperar.HPA.Shared.Models.Hattrick.Avatars
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Team = new Team();
        }

        public Team Team { get; set; }
    }
}