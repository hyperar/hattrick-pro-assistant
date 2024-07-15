namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    public class OwningTeam
    {
        public long LeagueId { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}