namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
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
