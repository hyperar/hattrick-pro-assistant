namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    public class SupportedTeam
    {
        [XmlElement("UserId")]
        public uint UserId { get; set; }

        [XmlElement("LoginName")]
        public string LoginName { get; set; } = string.Empty;

        [XmlElement("TeamId")]
        public uint TeamId { get; set; }

        [XmlElement("TeamName")]
        public string TeamName { get; set; } = string.Empty;

        [XmlElement("LeagueID")]
        public uint LeagueId { get; set; }

        [XmlElement("LeagueName")]
        public string LeagueName { get; set; } = string.Empty;

        [XmlElement("LeagueLevelUnitID")]
        public uint LeagueLevelUnitId { get; set; }

        [XmlElement("LeagueLevelUnitName")]
        public string LeagueLevelUnitName { get; set; } = string.Empty;

        [XmlElement("LastMatch")]
        public LastMatch LastMatch { get; set; } = new LastMatch();

        [XmlElement("NextMatch")]
        public NextMatch NextMatch { get; set; } = new NextMatch();
    }
}
