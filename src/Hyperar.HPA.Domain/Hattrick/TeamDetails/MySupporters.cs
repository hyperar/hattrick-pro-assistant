namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
