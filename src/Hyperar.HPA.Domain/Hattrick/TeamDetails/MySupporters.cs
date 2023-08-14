namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("MySupporters")]
    public class MySupporters
    {
        [XmlAttribute("TotalItems")]
        public uint TotalItems { get; set; }

        [XmlAttribute("MaxItems")]
        public uint MaxItems { get; set; }

        [XmlElement("SupporterTeam")]
        public List<SupporterTeam> SupporterTeamList { get; set; } = new List<SupporterTeam>();
    }
}
