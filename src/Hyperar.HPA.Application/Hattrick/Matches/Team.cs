namespace Hyperar.HPA.Application.Hattrick.Matches
{
    using System.Collections.Generic;

    public class Team
    {
        public League League { get; set; } = new League();

        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public List<Match> MatchList { get; set; } = new List<Match>();

        public string ShortTeamName { get; set; } = string.Empty;

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}