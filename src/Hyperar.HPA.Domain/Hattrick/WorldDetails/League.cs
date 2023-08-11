namespace Hyperar.HPA.Domain.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("League")]
    public class League
    {
        [XmlElement("LeagueID")]
        public uint LeagueId { get; set; }

        [XmlElement("LeagueName")]
        public string LeagueName { get; set; } = string.Empty;

        [XmlElement("Season")]
        public uint Season { get; set; }

        [XmlElement("SeasonOffset")]
        public int SeasonOffset { get; set; }

        [XmlElement("MatchRound")]
        public uint MatchRound { get; set; }

        [XmlElement("ShortName")]
        public string ShortName { get; set; } = string.Empty;

        [XmlElement("Continent")]
        public string Continent { get; set; } = string.Empty;

        [XmlElement("ZoneName")]
        public string ZoneName { get; set; } = string.Empty;

        [XmlElement("EnglishName")]
        public string EnglishName { get; set; } = string.Empty;

        [XmlElement("LanguageId")]
        public uint LanguageId { get; set; }

        [XmlElement("LanguageName")]
        public string LanguageName { get; set; } = string.Empty;

        [XmlElement("Country")]
        public Country Country { get; set; } = new Country();

        [XmlArray("Cups"), XmlArrayItem("Cup")]
        public List<Cup> Cups { get; set; } = new List<Cup>();

        [XmlElement("NationalTeamId")]
        public uint NationalTeamId { get; set; }

        [XmlElement("U20TeamId")]
        public uint U20TeamId { get; set; }

        [XmlElement("ActiveTeams")]
        public uint ActiveTeams { get; set; }

        [XmlElement("ActiveUsers")]
        public uint ActiveUsers { get; set; }

        [XmlElement("WaitingUsers")]
        public uint WaitingUsers { get; set; }

        [XmlElement("TrainingDate")]
        public string TrainingDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime TrainingDate
        {
            get
            {
                return DateTime.Parse(this.TrainingDateString);
            }
        }

        [XmlElement("EconomyDate")]
        public string EconomyDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime EconomyDate
        {
            get
            {
                return DateTime.Parse(this.EconomyDateString);
            }
        }

        [XmlElement("CupMatchDate")]
        public string CupMatchDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime CupMatchDate
        {
            get
            {
                return DateTime.Parse(this.CupMatchDateString);
            }
        }

        [XmlElement("SeriesMatchDate")]
        public string SeriesMatchDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime SeriesMatchDate
        {
            get
            {
                return DateTime.Parse(this.SeriesMatchDateString);
            }
        }

        [XmlElement("Sequence1")]
        public string Sequence1String { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime Sequence1
        {
            get
            {
                return DateTime.Parse(this.Sequence1String);
            }
        }

        [XmlElement("Sequence2")]
        public string Sequence2String { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime Sequence2
        {
            get
            {
                return DateTime.Parse(this.Sequence2String);
            }
        }

        [XmlElement("Sequence3")]
        public string Sequence3String { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime Sequence3
        {
            get
            {
                return DateTime.Parse(this.Sequence3String);
            }
        }

        [XmlElement("Sequence5")]
        public string Sequence5String { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime Sequence5
        {
            get
            {
                return DateTime.Parse(this.Sequence5String);
            }
        }

        [XmlElement("Sequence7")]
        public string Sequence7String { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime Sequence7
        {
            get
            {
                return DateTime.Parse(this.Sequence7String);
            }
        }

        [XmlElement("NumberOfLevels")]
        public uint NumberOfLeves { get; set; }
    }
}
