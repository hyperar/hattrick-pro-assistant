namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("LastMatch")]
    public class LastMatch
    {
        [XmlElement("LastMatchId")]
        public uint LastMatchId { get; set; }

        [XmlElement("LastMatchDate")]
        public string LastMatchDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime LastMatchDate
        {
            get
            {
                return DateTime.Parse(this.LastMatchDateString);
            }
        }

        [XmlElement("LastMatchHomeTeamId")]
        public uint LastMatchHomeTeamId { get; set; }

        [XmlElement("LastMatchHomeTeamName")]
        public string LastMatchHomeTeamName { get; set; } = string.Empty;

        [XmlElement("LastMatchHomeGoals")]
        public uint LastMatchHomeGoals { get; set; }

        [XmlElement("LastMatchAwayTeamId")]
        public uint LastMatchAwayTeamId { get; set; }

        [XmlElement("LastMatchAwayTeamName")]
        public string LastMatchAwayTeamName { get; set; } = string.Empty;

        [XmlElement("LastMatchAwayGoals")]
        public uint LastMatchAwayGoals { get; set; }
    }
}
