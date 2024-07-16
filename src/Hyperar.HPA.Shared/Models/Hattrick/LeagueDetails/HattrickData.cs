namespace Hyperar.HPA.Shared.Models.Hattrick.LeagueDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public int CurrentMatchRound { get; set; }

        public long LeagueId { get; set; }

        public int LeagueLevel { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public string LeagueName { get; set; } = string.Empty;

        public int MaxLevel { get; set; }

        public int Rank { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();
    }
}