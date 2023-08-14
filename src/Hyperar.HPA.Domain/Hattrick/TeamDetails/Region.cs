namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("Region")]
    public class Region
    {
        [XmlElement("RegionID")]
        public uint RegionId { get; set; }

        [XmlElement("RegionName")]
        public string RegionName { get; set; } = string.Empty;
    }
}
