namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Arena")]
    public class Arena
    {
        [XmlElement("ArenaID")]
        public uint ArenaId { get; set; }

        [XmlElement("ArenaName")]
        public string ArenaName { get; set; } = string.Empty;
    }
}
