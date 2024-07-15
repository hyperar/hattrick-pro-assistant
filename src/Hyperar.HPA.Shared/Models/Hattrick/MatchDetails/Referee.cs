namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Referee
    {
        public long RefereeCountryId { get; set; }

        public string RefereeCountryName { get; set; } = string.Empty;

        public long RefereeId { get; set; }

        public string RefereeName { get; set; } = string.Empty;

        public long RefereeTeamId { get; set; }

        public string RefereeTeamName { get; set; } = string.Empty;
    }
}