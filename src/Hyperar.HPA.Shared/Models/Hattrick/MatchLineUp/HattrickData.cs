namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Arena = new Arena();
            this.AwayTeam = new IdName();
            this.HomeTeam = new IdName();
            this.Team = new Team();

            this.SourceSystem = string.Empty;
        }

        public Arena Arena { get; set; }

        public IdName AwayTeam { get; set; }

        public IdName HomeTeam { get; set; }

        public long MatchContextId { get; set; }

        public DateTime MatchDate { get; set; }

        public long MatchId { get; set; }

        public byte MatchType { get; set; }

        public string SourceSystem { get; set; }

        public Team Team { get; set; }
    }
}