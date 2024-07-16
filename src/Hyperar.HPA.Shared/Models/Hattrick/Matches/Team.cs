namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    using System.Collections.Generic;

    public class Team
    {
        public IdName League { get; set; } = new IdName();

        public LeagueLevelUnit? LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public List<Match> MatchList { get; set; } = new List<Match>();

        public string? ShortTeamName { get; set; } = string.Empty;

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}