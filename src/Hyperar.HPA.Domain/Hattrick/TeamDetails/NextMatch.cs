namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("NextMatch")]
    public class NextMatch
    {
        [XmlElement("NextMatchId")]
        public uint NextMatchId { get; set; }

        [XmlElement("NextMatchMatchDate")]
        public string NextMatchMatchDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime NextMatchMatchDate
        {
            get
            {
                return DateTime.Parse(this.NextMatchMatchDateString);
            }
        }

        [XmlElement("NextMatchHomeTeamId")]
        public uint NextMatchHomeTeamId { get; set; }

        [XmlElement("NextMatchHomeTeamName")]
        public string NextMatchHomeTeamName { get; set; } = string.Empty;

        [XmlElement("NextMatchAwayTeamId")]
        public uint NextMatchAwayTeamId { get; set; }

        [XmlElement("NextMatchAwayTeamName")]
        public string NextMatchAwayTeamName { get; set; } = string.Empty;
    }
}
