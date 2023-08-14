namespace Hyperar.HPA.Domain.Hattrick.WorldDetails
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("Cup")]
    public class Cup
    {
        [XmlElement("CupID")]
        public uint CupId { get; set; }

        [XmlElement("CupName")]
        public string CupName { get; set; } = string.Empty;

        [XmlElement("CupLeagueLevel")]
        public uint CupLeagueLevel { get; set; }

        [XmlElement("CupLevel")]
        public uint CupLevel { get; set; }

        [XmlElement("CupLevelIndex")]
        public uint CupLevelIndex { get; set; }

        [XmlElement("MatchRound")]
        public uint MatchRound { get; set; }

        [XmlElement("MatchRoundsLeft")]
        public uint MatchRoundsLeft { get; set; }
    }
}
