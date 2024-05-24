namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Referee
    {
        public Referee()
        {
            this.RefereeCountryName = string.Empty;
            this.RefereeName = string.Empty;
            this.RefereeTeamName = string.Empty;
        }

        public long RefereeCountryId { get; set; }

        public string RefereeCountryName { get; set; }

        public long RefereeId { get; set; }

        public string RefereeName { get; set; }

        public long RefereeTeamId { get; set; }

        public string RefereeTeamName { get; set; }
    }
}