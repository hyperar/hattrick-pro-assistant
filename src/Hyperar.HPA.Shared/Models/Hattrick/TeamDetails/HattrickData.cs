namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
            this.Teams = new List<Team>();
            this.User = new User();
        }

        public List<Team> Teams { get; set; }

        public User User { get; set; }
    }
}