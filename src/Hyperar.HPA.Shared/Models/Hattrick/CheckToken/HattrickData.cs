namespace Hyperar.HPA.Shared.Models.Hattrick.CheckToken
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Token = string.Empty;

            this.ExtendedPermissions = new List<string>();
        }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public List<string> ExtendedPermissions { get; set; }

        public string Token { get; set; }

        public long User { get; set; }
    }
}