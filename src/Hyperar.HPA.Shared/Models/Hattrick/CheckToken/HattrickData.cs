namespace Hyperar.HPA.Shared.Models.Hattrick.CheckToken
{
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        { }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public string[] ExtendedPermissions { get; set; } = Array.Empty<string>();

        public string Token { get; set; } = string.Empty;

        public long User { get; set; }
    }
}