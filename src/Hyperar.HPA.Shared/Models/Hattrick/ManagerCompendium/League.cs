namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    public class League
    {
        public League()
        {
            this.LeagueName = string.Empty;
        }

        public long LeagueId { get; set; }

        public string LeagueName { get; set; }

        public int Season { get; set; }
    }
}