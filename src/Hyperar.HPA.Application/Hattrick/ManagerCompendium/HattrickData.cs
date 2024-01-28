namespace Hyperar.HPA.Application.Hattrick.ManagerCompendium
{
    using System;
    using Application.Hattrick;
    using Application.Hattrick.Interfaces;

    [Serializable]
    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public Manager Manager { get; set; } = new Manager();
    }
}