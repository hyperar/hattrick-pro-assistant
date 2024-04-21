namespace Hyperar.HPA.Shared.Models.Hattrick.WorldDetails
{
    using System.Collections.Generic;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.LeagueList = new List<League>();
        }

        public List<League> LeagueList { get; set; }
    }
}