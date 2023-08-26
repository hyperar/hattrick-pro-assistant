namespace Hyperar.HPA.Domain.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class HattrickData : XmlFileBase
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        [XmlArray("LeagueList"), XmlArrayItem("League")]
        public List<League> LeagueList { get; set; } = new List<League>();
    }
}
