namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.League = new IdName();
            this.LeagueLevelUnit = new LeagueLevelUnit();
            this.MatchList = new List<Match>();

            this.ShortTeamName = string.Empty;
            this.TeamName = string.Empty;
        }

        public IdName League { get; set; }

        public LeagueLevelUnit LeagueLevelUnit { get; set; }

        public List<Match> MatchList { get; set; }

        public string ShortTeamName { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}