namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class Flags
    {
        public List<Flag> AwayFlags { get; set; } = new List<Flag>();

        public List<Flag> HomeFlags { get; set; } = new List<Flag>();
    }
}