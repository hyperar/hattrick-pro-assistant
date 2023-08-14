namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("Team")]
    public class Team
    {
        [XmlElement("TeamId")]
        public uint TeamId { get; set; }

        [XmlElement("TeamName")]
        public string TeamName { get; set; } = string.Empty;

        [XmlElement("Arena")]
        public Arena Arena { get; set; } = new Arena();

        [XmlElement("League")]
        public League League { get; set; } = new League();

        [XmlElement("Country")]
        public Country Country { get; set; } = new Country();

        [XmlElement("LeagueLevelUnit")]
        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        [XmlElement("Region")]
        public Region Region { get; set; } = new Region();

        [XmlElement("YouthTeam")]
        public YouthTeam YouthTeam { get; set; } = new YouthTeam();

    }
}
