namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("TeamColors")]
    public class TeamColors
    {
        [XmlElement("BackgroundColor")]
        public string BackgroundColor { get; set; } = string.Empty;

        [XmlElement("Color")]
        public string Color { get; set; } = string.Empty;
    }
}
