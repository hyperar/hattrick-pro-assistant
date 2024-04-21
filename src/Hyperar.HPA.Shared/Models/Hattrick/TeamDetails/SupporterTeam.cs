namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class SupporterTeam
    {
        public SupporterTeam()
        {
            this.LeagueLevelUnitName = string.Empty;
            this.LeagueName = string.Empty;
            this.LoginName = string.Empty;
            this.TeamName = string.Empty;
        }

        public long LeagueId { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; }

        public string LeagueName { get; set; }

        public string LoginName { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public long UserId { get; set; }
    }
}