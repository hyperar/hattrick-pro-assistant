namespace Hyperar.HPA.Shared.Models.Hattrick.MatchArchive
{
    using System.Collections.Generic;

    public class Team
    {
        public List<Match> MatchList { get; set; } = new List<Match>();

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}