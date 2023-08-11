namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Guestbook")]
    public class Guestbook
    {
        [XmlElement("NumberOfGuestbookItems")]
        public uint NumberOfGuestbookItems { get; set; }
    }
}
