namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System.Collections.Generic;
    using Application.Hattrick;
    using Application.Hattrick.Interfaces;

    public class HattrickData : XmlFileBase, IXmlFile
    {
        public HattrickData(string fileName) : base(fileName)
        {
        }

        public List<Team> Teams { get; set; } = new List<Team>();

        public User User { get; set; } = new User();
    }
}