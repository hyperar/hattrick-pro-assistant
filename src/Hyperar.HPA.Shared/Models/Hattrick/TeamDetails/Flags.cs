namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class Flags
    {
        public Flags()
        {
            this.AwayFlags = new List<Flag>();
            this.HomeFlags = new List<Flag>();
        }

        public List<Flag> AwayFlags { get; set; }

        public List<Flag> HomeFlags { get; set; }
    }
}