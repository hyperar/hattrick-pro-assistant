namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("NationalTeam")]
    public class NationalTeam
    {
        [XmlElement("NationalTeamId")]
        public uint NationalTeamId { get; set; }

        [XmlElement("NationalTeamName")]
        public string NationalTeamName { get; set; } = string.Empty;
    }
}
