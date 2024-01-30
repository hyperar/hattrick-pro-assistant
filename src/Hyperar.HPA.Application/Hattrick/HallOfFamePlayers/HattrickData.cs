namespace Hyperar.HPA.Application.Hattrick.HallOfFamePlayers
{
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public List<Player> PlayerList { get; set; } = new List<Player>();
    }
}