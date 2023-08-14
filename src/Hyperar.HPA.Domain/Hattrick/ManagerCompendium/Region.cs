namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("Region")]
    public class Region
    {
        [XmlElement("RegionId")]
        public uint RegionId { get; set; }

        [XmlElement("RegionName")]
        public string RegionName { get; set; } = string.Empty;
    }
}
