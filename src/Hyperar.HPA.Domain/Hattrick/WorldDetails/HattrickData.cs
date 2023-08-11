namespace Hyperar.HPA.Domain.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [Serializable]
    public class HattrickData : XmlFileBase
    {
        [XmlArray("LeagueList"), XmlArrayItem("League")]
        public List<League> LeagueList { get; set; } = new List<League>();
    }
}
