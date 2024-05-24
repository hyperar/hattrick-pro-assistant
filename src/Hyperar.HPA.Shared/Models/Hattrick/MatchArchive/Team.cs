namespace Hyperar.HPA.Shared.Models.Hattrick.MatchArchive
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.MatchList = new List<Match>();

            this.TeamName = string.Empty;
        }

        public List<Match> MatchList { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}