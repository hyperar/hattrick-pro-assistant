namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Language")]
    public class Language
    {
        [XmlElement("LanguageID")]
        public uint LanguageId { get; set; }

        [XmlElement("LanguageName")]
        public string LanguageName { get; set; } = string.Empty;
    }
}
