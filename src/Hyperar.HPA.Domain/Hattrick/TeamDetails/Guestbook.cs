namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Xml.Serialization;

    [XmlRoot("Guestbook")]
    public class Guestbook
    {
        [XmlElement("NumberOfGuestbookItems")]
        public uint NumberOfGuestbookItems { get; set; }
    }
}
