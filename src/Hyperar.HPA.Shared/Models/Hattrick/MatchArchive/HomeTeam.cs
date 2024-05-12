namespace Hyperar.HPA.Shared.Models.Hattrick.MatchArchive
{
    public class HomeTeam
    {
        public HomeTeam()
        {
            this.HomeTeamName = string.Empty;
        }

        public long HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }
    }
}