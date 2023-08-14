namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("LeagueLevelUnit")]
    public class LeagueLevelUnit
    {
        [XmlElement("LeagueLevelUnitId")]
        public uint LeagueLevelUnitId { get; set; }

        [XmlElement("LeagueLevelUnitName")]
        public string LeagueLevelUnitName { get; set; } = string.Empty;
    }
}
