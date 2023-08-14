namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("Flag")]
    public class Flag
    {
        [XmlElement("LeagueId")]
        public uint LeagueId { get; set; }

        [XmlElement("LeagueName")]
        public string LeagueName { get; set; } = string.Empty;

        [XmlElement("CountryCode")]
        public string CountryCode { get; set; } = string.Empty;
    }
}
