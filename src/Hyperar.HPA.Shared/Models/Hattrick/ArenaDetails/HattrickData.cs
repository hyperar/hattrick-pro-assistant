namespace Hyperar.HPA.Shared.Models.Hattrick.ArenaDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public Arena Arena { get; set; } = new Arena();
    }
}