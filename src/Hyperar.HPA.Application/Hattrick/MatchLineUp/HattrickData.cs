namespace Hyperar.HPA.Application.Hattrick.MatchLineUp
{
    using Application.Hattrick.Interfaces;
    using Common.Enums;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public Arena Arena { get; set; } = new Arena();

        public AwayTeam AwayTeam { get; set; } = new AwayTeam();

        public HomeTeam HomeTeam { get; set; } = new HomeTeam();

        public uint MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public uint MatchId { get; set; }

        public MatchType MatchType { get; set; }

        public string SourceSystem { get; set; } = string.Empty;

        public Team Team { get; set; } = new Team();
    }
}