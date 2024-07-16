namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public Match Match { get; set; } = new Match();

        public string SourceSystem { get; set; } = string.Empty;

        public string UserSupporterTier { get; set; } = string.Empty;
    }
}