namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    public class HomeTeam
    {
        public HomeTeam()
        {
            this.HomeTeamName = string.Empty;
            this.HomeTeamShortName = string.Empty;
        }

        public long HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public string HomeTeamShortName { get; set; }
    }
}