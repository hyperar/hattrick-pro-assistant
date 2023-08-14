namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;
    using Hyperar.HPA.Common.Enums;

    [XmlRoot("NationalTeam")]
    public class NationalTeam
    {
        [XmlAttribute("Index")]
        public uint Index { get; set; }

        [XmlElement("NationalTeamStaffType")]
        public uint NationalTeamStaffTypeUint { get; set; }

        [XmlIgnore]
        public NationalTeamStaffType NationalTeamStaffType
        {
            get
            {
                return (NationalTeamStaffType)this.NationalTeamStaffTypeUint;
            }
        }

        [XmlElement("NationalTeamID")]
        public uint NationalTeamId { get; set; }

        [XmlElement("NationalTeamName")]
        public string NationalTeamName { get; set; } = string.Empty;
    }
}
