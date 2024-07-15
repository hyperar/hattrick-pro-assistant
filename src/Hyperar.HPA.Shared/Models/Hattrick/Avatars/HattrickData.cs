namespace Hyperar.HPA.Shared.Models.Hattrick.Avatars
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public Team Team { get; set; } = new Team();
    }
}