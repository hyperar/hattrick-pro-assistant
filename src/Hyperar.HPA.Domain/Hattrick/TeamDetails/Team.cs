namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("Team")]
    public class Team
    {
        [XmlElement("TeamID")]
        public uint TeamId { get; set; }

        [XmlElement("TeamName")]
        public string TeamName { get; set; } = string.Empty;

        [XmlElement("ShortTeamName")]
        public string ShortTeamName { get; set; } = string.Empty;

        [XmlElement("IsPrimaryClub")]
        public string IsPrimaryClubString { get; set; } = string.Empty;

        [XmlIgnore]
        public bool IsPrimaryClub
        {
            get
            {
                return bool.Parse(this.IsPrimaryClubString.ToLower());
            }
        }

        [XmlElement("FoundedDate")]
        public string FoundedDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime FoundedDate
        {
            get
            {
                return DateTime.Parse(this.FoundedDateString);
            }
        }

        [XmlElement("Arena")]
        public Arena Arena { get; set; } = new Arena();

        [XmlElement("League")]
        public League League { get; set; } = new League();

        [XmlElement("Country")]
        public Country Country { get; set; } = new Country();

        [XmlElement("Region")]
        public Region Region { get; set; } = new Region();

        [XmlElement("Trainer")]
        public Trainer Trainer { get; set; } = new Trainer();

        [XmlElement("HomePage")]
        public string HomePage { get; set; } = string.Empty;

        [XmlElement("DressURI")]
        public string DressUri { get; set; } = string.Empty;

        [XmlElement("DressAlternateURI")]
        public string DressAlternateUri { get; set; } = string.Empty;

        [XmlElement("LeagueLevelUnit")]
        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        [XmlElement("BotStatus")]
        public BotStatus BotStatus { get; set; } = new BotStatus();

        [XmlElement("Cup")]
        public Cup Cup { get; set; } = new Cup();

        [XmlElement("PowerRating")]
        public Rating PowerRating { get; set; } = new Rating();

        [XmlElement("FriendlyTeamID")]
        public uint FriendlyTeamId { get; set; }

        [XmlElement("NumberOfVictories")]
        public uint NumberOfVictories { get; set; }

        [XmlElement("NumberOfUndefeated")]
        public uint NumberOfUndefeated { get; set; }

        [XmlElement("TeamRank")]
        public uint TeamRank { get; set; }

        [XmlElement("Fanclub")]
        public Fanclub Fanclub { get; set; } = new Fanclub();

        [XmlElement("LogoURL")]
        public string LogoUrl { get; set; } = string.Empty;

        [XmlElement("Guestbook")]
        public Guestbook Guestbook { get; set; } = new Guestbook();

        [XmlElement("PressAnnouncement")]
        public PressAnnouncement PressAnnouncement { get; set; } = new PressAnnouncement();

        [XmlElement(ElementName = "TeamColors", IsNullable = true)]
        public TeamColors? TeamColors { get; set; } = null;

        [XmlElement("YouthTeamID")]
        public uint YouthTeamId { get; set; }

        [XmlElement("YouthTeamName")]
        public string YouthTeamName { get; set; } = string.Empty;

        [XmlElement("NumberOfVisits")]
        public uint NumberOfVisits { get; set; }

        [XmlElement("Flags")]
        public Flags Flags { get; set; } = new Flags();

        [XmlArray(ElementName = "TrophyList", IsNullable = true), XmlArrayItem("Trophy")]
        public List<Trophy>? TrophyList { get; set; } = null;

        [XmlElement("SupportedTeams")]
        public SupportedTeams SupportedTeams { get; set; } = new SupportedTeams();

        [XmlElement("MySupporters")]
        public MySupporters MySupporters { get; set; } = new MySupporters();

        [XmlElement("PossibleToChallengeMidweek")]
        public string PossibleToChallengeMidweekString { get; set; } = string.Empty;

        [XmlIgnore]
        public bool PossibleToChallengeMidweek
        {
            get
            {
                return bool.Parse(this.PossibleToChallengeMidweekString.ToLower());
            }
        }

        [XmlElement("PossibleToChallengeWeekend")]
        public string PossibleToChallengeWeekendString { get; set; } = string.Empty;

        [XmlIgnore]
        public bool PossibleToChallengeWeekend
        {
            get
            {
                return bool.Parse(this.PossibleToChallengeWeekendString.ToLower());
            }
        }
    }
}
