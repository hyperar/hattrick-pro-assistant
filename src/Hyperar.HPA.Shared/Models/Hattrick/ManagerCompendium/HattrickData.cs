namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public Manager Manager { get; set; } = new Manager();
    }
}