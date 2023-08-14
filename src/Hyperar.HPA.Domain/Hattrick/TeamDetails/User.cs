namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("User")]
    public class User
    {
        [XmlElement("UserID")]
        public uint UserId { get; set; }

        [XmlElement("Language")]
        public Language Language { get; set; } = new Language();

        [XmlElement("SupporterTier")]
        public string SupporterTierString { get; set; } = string.Empty;

        [XmlIgnore]
        public Common.Enums.SupporterTier SupporterTier
        {
            get
            {
                switch (this.SupporterTierString)
                {
                    case Common.Constants.SupporterTier.None:
                        return Common.Enums.SupporterTier.None;

                    case Common.Constants.SupporterTier.Silver:
                        return Common.Enums.SupporterTier.Silver;

                    case Common.Constants.SupporterTier.Gold:
                        return Common.Enums.SupporterTier.Gold;

                    case Common.Constants.SupporterTier.Platinum:
                        return Common.Enums.SupporterTier.Platinum;

                    case Common.Constants.SupporterTier.Diamond:
                        return Common.Enums.SupporterTier.Diamond;

                    default:
                        return Common.Enums.SupporterTier.None;
                }
            }
        }

        [XmlElement("Loginname")]
        public string LoginName { get; set; } = string.Empty;

        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("ICQ")]
        public string Icq { get; set; } = string.Empty;

        [XmlElement("SignupDate")]
        public string SignUpDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime SignUpDate
        {
            get
            {
                return DateTime.Parse(this.SignUpDateString);
            }
        }

        [XmlElement("ActivationDate")]
        public string ActivationDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime ActivationDate
        {
            get
            {
                return DateTime.Parse(this.ActivationDateString);
            }
        }

        [XmlElement("LastLoginDate")]
        public string LastLoginDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime LastLoginDate
        {
            get
            {
                return DateTime.Parse(this.LastLoginDateString);
            }
        }

        [XmlElement("HasManagerLicense")]
        public string HasManagerLicenseString { get; set; } = string.Empty;

        [XmlIgnore]
        public bool HasManagerLicense
        {
            get
            {
                return bool.Parse(this.HasManagerLicenseString.ToLower());
            }
        }

        [XmlArray("NationalTeams"), XmlArrayItem("NationalTeam")]
        public List<NationalTeam> NationalTeams { get; set; } = new List<NationalTeam>();
    }
}
