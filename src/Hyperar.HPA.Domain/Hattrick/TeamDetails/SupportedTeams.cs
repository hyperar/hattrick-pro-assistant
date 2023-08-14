namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("SupportedTeams")]
    public class SupportedTeams
    {
        [XmlAttribute("TotalItems")]
        public uint TotalItems { get; set; }

        [XmlAttribute("MaxItems")]
        public uint MaxItems { get; set; }

        [XmlElement("SupportedTeam")]
        public List<SupportedTeam> SupportedTeamList { get; set; } = new List<SupportedTeam>();
    }
}
