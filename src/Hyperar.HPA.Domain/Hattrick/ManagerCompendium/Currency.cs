namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Currency")]
    public class Currency
    {
        [XmlElement("CurrencyName")]
        public string CurrencyName { get; set; } = string.Empty;

        [XmlElement("CurrencyRate")]
        public string CurrencyRateString { get; set; } = string.Empty;

        [XmlIgnore]
        public decimal CurrencyRate
        {
            get
            {
                return decimal.Parse(
                    this.CurrencyRateString.Replace(
                        ",",
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
        }
    }
}
