namespace Hyperar.HPA.Shared.Models.Hattrick.Players
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Team = new Team();

            this.ActionType = string.Empty;
            this.UserSupporterTier = string.Empty;
        }

        public string ActionType { get; set; }

        public bool IsPlayingMatch { get; set; }

        public bool IsYouth { get; set; }

        public Team Team { get; set; }

        public string UserSupporterTier { get; set; }
    }
}