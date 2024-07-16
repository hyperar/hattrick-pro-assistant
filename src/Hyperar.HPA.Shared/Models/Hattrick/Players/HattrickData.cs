namespace Hyperar.HPA.Shared.Models.Hattrick.Players
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public string ActionType { get; set; } = string.Empty;

        public bool IsPlayingMatch { get; set; }

        public bool IsYouth { get; set; }

        public Team Team { get; set; } = new Team();

        public string UserSupporterTier { get; set; } = string.Empty;
    }
}