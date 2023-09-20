namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    public class SupporterTeam
    {
        public uint LeagueId { get; set; }

        public uint LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public string LeagueName { get; set; } = string.Empty;

        public string LoginName { get; set; } = string.Empty;

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public uint UserId { get; set; }
    }
}