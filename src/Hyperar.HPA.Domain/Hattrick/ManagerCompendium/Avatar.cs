namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Avatar")]
    public class Avatar
    {
        [XmlElement("BackgroundImage")]
        public string BackgroundImage { get; set; } = string.Empty;

        [XmlElement("Layer")]
        public List<Layer> Layers { get; set; } = new List<Layer>();
    }
}
