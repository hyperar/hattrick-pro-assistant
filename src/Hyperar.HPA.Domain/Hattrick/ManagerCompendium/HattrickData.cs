namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class HattrickData : XmlFileBase
    {
        [XmlElement("Manager")]
        public Manager Manager { get; set; } = new Manager();
    }
}
