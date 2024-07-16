namespace Hyperar.HPA.Shared.Models.Hattrick.YouthLeagueDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public int LastMatchRound { get; set; }

        public int NrOfTeamsInLeague { get; set; }

        public int Season { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();

        public long YouthLeagueId { get; set; }

        public string YouthLeagueName { get; set; } = string.Empty;

        public int YouthLeagueType { get; set; }
    }
}