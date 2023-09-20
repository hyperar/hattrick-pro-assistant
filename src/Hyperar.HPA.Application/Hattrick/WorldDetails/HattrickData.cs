namespace Hyperar.HPA.Application.Hattrick.WorldDetails
{
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick;
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public List<League> LeagueList { get; set; } = new List<League>();
    }
}