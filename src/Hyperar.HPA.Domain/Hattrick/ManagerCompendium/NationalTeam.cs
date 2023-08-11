namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("NationalTeam")]
    public class NationalTeam
    {
        [XmlElement("NationalTeamId")]
        public uint NationalTeamId { get; set; }

        [XmlElement("NationalTeamName")]
        public string NationalTeamName { get; set; } = string.Empty;
    }
}
