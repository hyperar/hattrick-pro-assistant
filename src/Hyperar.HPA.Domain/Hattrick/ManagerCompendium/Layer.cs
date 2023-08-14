namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Xml.Serialization;

    [XmlRoot("Layer")]
    public class Layer
    {
        [XmlAttribute("x")]
        public int X { get; set; }

        [XmlAttribute("y")]
        public int Y { get; set; }

        [XmlElement("Image")]
        public string Image { get; set; } = string.Empty;
    }
}
