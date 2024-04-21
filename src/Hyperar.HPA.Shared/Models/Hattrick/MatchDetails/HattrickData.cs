namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Match = new Match();
            this.SourceSystem = string.Empty;
            this.UserSupporterTier = string.Empty;
        }

        public Match Match { get; set; }

        public string SourceSystem { get; set; }

        public string UserSupporterTier { get; set; }
    }
}