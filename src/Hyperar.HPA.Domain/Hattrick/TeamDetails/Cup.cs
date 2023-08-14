namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("Cup")]
    public class Cup
    {
        [XmlElement("StillInCup")]
        public string StillInCupString { get; set; } = string.Empty;

        [XmlIgnore]
        public bool StillInCup
        {
            get
            {
                return bool.Parse(this.StillInCupString.ToLower());
            }
        }
    }
}
