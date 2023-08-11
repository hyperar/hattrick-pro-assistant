namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("PressAnnouncement")]
    public class PressAnnouncement
    {
        [XmlElement("Subject")]
        public string Subject { get; set; } = string.Empty;

        [XmlElement("Body")]
        public string Body { get; set; } = string.Empty;

        [XmlElement("SendDate")]
        public string SendDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime SendDate
        {
            get
            {
                return DateTime.Parse(this.SendDateString);
            }
        }
    }
}
