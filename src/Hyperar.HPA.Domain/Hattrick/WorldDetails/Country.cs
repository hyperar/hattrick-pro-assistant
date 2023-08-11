namespace Hyperar.HPA.Domain.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Country")]
    public class Country
    {
        [XmlAttribute("Available")]
        public string AvailableString { get; set; } = string.Empty;

        public bool Available
        {
            get
            {
                return bool.Parse(this.AvailableString.ToLower());
            }
        }

        [XmlElement("CountryID")]
        public uint? CountryId { get; set; }

        [XmlElement("CountryName")]
        public string? CountryName { get; set; }

        [XmlElement("CurrencyName")]
        public string? CurrencyName { get; set; }

        [XmlElement("CurrencyRate")]
        public string? CurrencyRateString { get; set; }

        [XmlIgnore]
        public decimal? CurrencyRate
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.CurrencyRateString)
                    ? null
                    : decimal.Parse(
                        this.CurrencyRateString.Replace(
                            ",",
                            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
        }

        [XmlElement("CountryCode")]
        public string? CountryCode { get; set; }

        [XmlElement("DateFormat")]
        public string? DateFormat { get; set; }

        [XmlElement("TimeFormat")]
        public string? TimeFormat { get; set; }

        [XmlArray("RegionList"), XmlArrayItem("Region")]
        public List<Region> RegionList { get; set; } = new List<Region>();

    }
}
