namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Manager")]
    public class Manager
    {
        [XmlElement("UserId")]
        public uint UserId { get; set; }

        [XmlElement("Loginname")]
        public string LoginName { get; set; } = string.Empty;

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

        [XmlArrayItem("LoginTime")]
        public List<string> LastLogins { get; set; } = new List<string>();

        [XmlElement("Language")]
        public Language Language { get; set; } = new Language();

        [XmlElement("Country")]
        public Country Country { get; set; } = new Country();

        [XmlElement("Currency")]
        public Currency Currency { get; set; } = new Currency();

        [XmlArray("Teams")]
        public List<Team> Teams { get; set; } = new List<Team>();

        [XmlArray("NationalTeamCoach")]
        public List<NationalTeam> NationalTeamCoach { get; set; } = new List<NationalTeam>();

        [XmlArray("NationalTeamAssistant")]
        public List<NationalTeam> NationalTeamAssistant { get; set; } = new List<NationalTeam>();

        [XmlElement("Avatar")]
        public Avatar Avatar { get; set; } = new Avatar();
    }
}
