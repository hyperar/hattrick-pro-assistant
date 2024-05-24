namespace Hyperar.HPA.Shared.Models.Hattrick.HallOfFamePlayers
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.PlayerList = new List<Player>();
        }

        public List<Player> PlayerList { get; set; }
    }
}