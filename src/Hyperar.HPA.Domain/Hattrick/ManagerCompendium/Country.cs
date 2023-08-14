namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("Country")]
    public class Country
    {
        [XmlElement("CountryId")]
        public uint CountryId { get; set; }

        [XmlElement("CountryName")]
        public string CountryName { get; set; } = string.Empty;
    }
}
