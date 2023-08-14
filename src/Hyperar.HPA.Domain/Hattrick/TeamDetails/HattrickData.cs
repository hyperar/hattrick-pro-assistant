namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("HattrickData")]
    public class HattrickData : XmlFileBase
    {
        [XmlElement("User")]
        public User User { get; set; } = new User();

        [XmlArray("Teams"), XmlArrayItem("Team")]
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
