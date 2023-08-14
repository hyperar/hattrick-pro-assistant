namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("Trainer")]
    public class Trainer
    {
        [XmlElement("PlayerID")]
        public uint PlayerId { get; set; }
    }
}
