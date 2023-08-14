namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("Language")]
    public class Language
    {
        [XmlElement("LanguageId")]
        public uint LanguageId { get; set; }

        [XmlElement("LanguageName")]
        public string LanguageName { get; set; } = string.Empty;
    }
}
