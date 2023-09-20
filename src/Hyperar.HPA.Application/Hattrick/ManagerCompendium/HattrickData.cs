namespace Hyperar.HPA.Application.Hattrick.ManagerCompendium
{
    using System;
    using System.Xml.Serialization;
    using Hyperar.HPA.Application.Hattrick;
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    [Serializable]
    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        [XmlElement("Manager")]
        public Manager Manager { get; set; } = new Manager();
    }
}