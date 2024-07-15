namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public bool IsPlayingMatch { get; set; }

        public Player Player { get; set; } = new Player();

        public string UserSupporterTier { get; set; } = string.Empty;
    }
}