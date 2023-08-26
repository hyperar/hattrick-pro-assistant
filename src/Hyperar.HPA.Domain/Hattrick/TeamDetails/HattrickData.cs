namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class HattrickData : XmlFileBase
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public User User { get; set; } = new User();

        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
