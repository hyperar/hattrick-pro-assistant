namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("YouthLeague")]
    public class YouthLeague
    {
        [XmlElement("YouthLeagueId")]
        public uint YouthLeagueId { get; set; }

        [XmlElement("YouthLeagueName")]
        public string YouthLeagueName { get; set; } = string.Empty;
    }
}
