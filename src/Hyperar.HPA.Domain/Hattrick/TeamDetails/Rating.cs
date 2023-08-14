namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("PowerRating")]
    public class Rating
    {
        [XmlElement("GlobalRanking")]
        public uint GlobalRanking { get; set; }

        [XmlElement("LeagueRanking")]
        public uint LeagueRanking { get; set; }

        [XmlElement("RegionRanking")]
        public uint RegionRanking { get; set; }

        [XmlElement("PowerRating")]
        public uint PowerRating { get; set; }
    }
}
