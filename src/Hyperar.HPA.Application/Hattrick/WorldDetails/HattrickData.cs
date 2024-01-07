namespace Hyperar.HPA.Application.Hattrick.WorldDetails
{
    using System.Collections.Generic;
    using Application.Hattrick;
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public List<League> LeagueList { get; set; } = new List<League>();
    }
}