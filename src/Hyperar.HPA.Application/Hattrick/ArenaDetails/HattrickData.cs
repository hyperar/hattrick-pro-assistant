namespace Hyperar.HPA.Application.Hattrick.ArenaDetails
{
    using Hyperar.HPA.Application.Hattrick;
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public Arena Arena { get; set; } = new Arena();
    }
}