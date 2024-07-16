namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerDetails
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public YouthPlayer YouthPlayer { get; set; } = new YouthPlayer();
    }
}