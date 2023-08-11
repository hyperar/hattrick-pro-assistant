namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("League")]
    public class League
    {
        [XmlElement("LeagueID")]
        public uint LeagueId { get; set; }

        [XmlElement("LeagueName")]
        public string LeagueName { get; set; } = string.Empty;
    }
}
