namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public IdName Arena { get; set; } = new IdName();

        public IdName AwayTeam { get; set; } = new IdName();

        public IdName HomeTeam { get; set; } = new IdName();

        public long? MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public int MatchType { get; set; }

        public string SourceSystem { get; set; } = string.Empty;

        public Team Team { get; set; } = new Team();
    }
}