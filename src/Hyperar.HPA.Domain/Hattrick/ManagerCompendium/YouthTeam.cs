namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("YouthTeam")]
    public class YouthTeam
    {
        [XmlElement("YouthTeamId")]
        public uint YouthTeamId { get; set; }

        [XmlElement("YouthTeamName")]
        public string YouthTeamName { get; set; } = string.Empty;

        [XmlElement("YouthLeague")]
        public YouthLeague YouthLeague { get; set; } = new YouthLeague();
    }
}
