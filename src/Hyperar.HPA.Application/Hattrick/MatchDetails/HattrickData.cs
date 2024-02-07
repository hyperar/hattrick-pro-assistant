namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Application.Hattrick.Interfaces;
    using Hyperar.HPA.Common.Enums;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public Match Match { get; set; } = new Match();

        public string SourceSystem { get; set; } = string.Empty;

        public string UserSupporterTier { get; set; }
    }
}