namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Country")]
    public class Country
    {
        [XmlElement("CountryID")]
        public uint CountryId { get; set; }

        [XmlElement("CountryName")]
        public string CountryName { get; set; } = string.Empty;
    }
}
