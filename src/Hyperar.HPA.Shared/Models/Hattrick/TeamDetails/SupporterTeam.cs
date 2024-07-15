namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class SupporterTeam
    {
        public long LeagueId { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public string LeagueName { get; set; } = string.Empty;

        public string LoginName { get; set; } = string.Empty;

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public long UserId { get; set; }
    }
}