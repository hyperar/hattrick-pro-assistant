namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    public class OwningTeam
    {
        public OwningTeam()
        {
            this.TeamName = string.Empty;
        }

        public long LeagueId { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}