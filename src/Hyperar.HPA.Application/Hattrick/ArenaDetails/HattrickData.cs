namespace Hyperar.HPA.Application.Hattrick.ArenaDetails
{
    using Application.Hattrick;
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public Arena Arena { get; set; } = new Arena();
    }
}