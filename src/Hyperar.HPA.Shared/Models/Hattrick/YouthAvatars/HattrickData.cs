namespace Hyperar.HPA.Shared.Models.Hattrick.YouthAvatars
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public YouthTeam YouthTeam { get; set; } = new YouthTeam();
    }
}