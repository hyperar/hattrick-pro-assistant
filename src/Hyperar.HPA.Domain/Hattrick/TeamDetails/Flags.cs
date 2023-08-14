namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("Flags")]
    public class Flags
    {
        [XmlArray("AwayFlags"), XmlArrayItem("Flag")]
        public List<Flag> AwayFlags { get; set; } = new List<Flag>();

        [XmlArray("HomeFlags"), XmlArrayItem("Flag")]
        public List<Flag> HomeFlags { get; set; } = new List<Flag>();
    }
}
