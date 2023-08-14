namespace Hyperar.HPA.Domain.Hattrick
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("HattrickData")]
    public class XmlFileBase
    {
        [XmlElement("FileName")]
        public string FileName { get; set; } = string.Empty;

        [XmlElement("Version")]
        public string VersionString { get; set; } = string.Empty;

        [XmlIgnore]
        public decimal Version
        {
            get
            {
                return decimal.Parse(this.VersionString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
        }

        [XmlElement("UserID")]
        public uint UserId { get; set; }

        [XmlElement("FetchedDate")]
        public string FetchedDateString { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime FetchedDate
        {
            get
            {
                return DateTime.Parse(this.FetchedDateString);
            }
        }
    }
}
