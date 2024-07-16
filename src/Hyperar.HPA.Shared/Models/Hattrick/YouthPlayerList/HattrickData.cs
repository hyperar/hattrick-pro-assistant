namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerList
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public List<YouthPlayer> YouthPlayerList { get; set; } = new List<YouthPlayer>();
    }
}