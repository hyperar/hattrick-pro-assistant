namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("LeagueLevelUnit")]
    public class LeagueLevelUnit
    {
        [XmlElement("LeagueLevelUnitID")]
        public uint LeagueLevelUnitId { get; set; }

        [XmlElement("LeagueLevelUnitName")]
        public string LeagueLevelUnitName { get; set; } = string.Empty;

        [XmlElement("LeagueLevel")]
        public uint LeagueLevel { get; set; }
    }
}
