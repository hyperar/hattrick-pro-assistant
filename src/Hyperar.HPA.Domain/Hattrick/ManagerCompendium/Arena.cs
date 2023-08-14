namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
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
