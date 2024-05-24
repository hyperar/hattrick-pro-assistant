namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.UserSupporterTier = string.Empty;

            this.Player = new Player();
        }

        public bool IsPlayingMatch { get; set; }

        public Player Player { get; set; }

        public string UserSupporterTier { get; set; }
    }
}