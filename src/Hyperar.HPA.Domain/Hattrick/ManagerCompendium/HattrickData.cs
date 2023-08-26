namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class HattrickData : XmlFileBase
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        [XmlElement("Manager")]
        public Manager Manager { get; set; } = new Manager();
    }
}
