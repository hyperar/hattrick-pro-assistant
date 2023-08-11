namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Arena")]
    public class Arena
    {
        [XmlElement("ArenaId")]
        public uint ArenaId { get; set; }

        [XmlElement("ArenaName")]
        public string ArenaName { get; set; } = string.Empty;
    }
}
