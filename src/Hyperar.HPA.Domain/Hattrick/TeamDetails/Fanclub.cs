namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("Fanclub")]
    public class Fanclub
    {
        [XmlElement("FanclubID")]
        public uint FanclubId { get; set; }

        [XmlElement("FanclubName")]
        public string FanclubName { get; set; } = string.Empty;

        [XmlElement("FanclubSize")]
        public uint FanclubSize { get; set; }
    }
}
