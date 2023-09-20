namespace Hyperar.HPA.Application.Hattrick.Players
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Common.Enums;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public string ActionType { get; set; } = string.Empty;

        public bool IsPlayingMatch { get; set; }

        public bool IsYouth { get; set; }

        public Team Team { get; set; } = new Team();

        public SupporterTier UserSupporterTier { get; set; }
    }
}