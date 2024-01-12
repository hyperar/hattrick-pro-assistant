namespace Hyperar.HPA.Application.Hattrick.Avatars
{
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public Team Team { get; set; } = new Team();
    }
}