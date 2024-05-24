namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class Flag
    {
        public Flag()
        {
            this.CountryCode = string.Empty;
            this.LeagueName = string.Empty;
        }

        public string CountryCode { get; set; }

        public long LeagueId { get; set; }

        public string LeagueName { get; set; }
    }
}