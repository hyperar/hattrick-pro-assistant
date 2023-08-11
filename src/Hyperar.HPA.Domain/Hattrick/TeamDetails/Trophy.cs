namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Trophy")]
    public class Trophy
    {
        [XmlElement("TrophyTypeId")]
        public uint TrophyTypeId { get; set; }

        [XmlElement("TrophySeason")]
        public uint TrophySeason { get; set; }

        [XmlElement("LeagueLevel")]
        public uint LeagueLevel { get; set; }

        [XmlElement("LeagueLevelUnitId")]
        public uint LeagueLevelUnitId { get; set; }

        [XmlElement("LeagueLevelUnitName")]
        public string LeagueLevelUnitName { get; set; } = string.Empty;

        [XmlElement("GainedDate")]
        public string GainedDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime GainedDate
        {
            get
            {
                return DateTime.Parse(this.GainedDateString);
            }
        }

        [XmlElement(ElementName = "ImageUrl", IsNullable = true)]
        public string? ImageUrl { get; set; }

        [XmlElement("CupLeagueLevel")]
        public string CupLeagueLevelString { get; set; } = string.Empty;

        [XmlIgnore]
        public uint? CupLeagueLevel
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.CupLeagueLevelString)
                     ? null
                     : uint.Parse(this.CupLeagueLevelString);
            }
        }

        [XmlElement("CupLevel")]
        public string CupLevelString { get; set; } = string.Empty;

        [XmlIgnore]
        public uint? CupLevel
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.CupLevelString)
                     ? null
                     : uint.Parse(this.CupLevelString);
            }
        }

        [XmlElement("CupLevelIndex")]
        public string CupLevelIndexString { get; set; } = string.Empty;

        [XmlIgnore]
        public uint? CupLevelIndex
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.CupLevelIndexString)
                     ? null
                     : uint.Parse(this.CupLevelIndexString);
            }
        }
    }
}
