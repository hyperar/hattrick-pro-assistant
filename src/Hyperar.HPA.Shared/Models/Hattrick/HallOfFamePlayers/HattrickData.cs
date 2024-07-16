namespace Hyperar.HPA.Shared.Models.Hattrick.HallOfFamePlayers
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public List<Player> PlayerList { get; set; } = new List<Player>();
    }
}