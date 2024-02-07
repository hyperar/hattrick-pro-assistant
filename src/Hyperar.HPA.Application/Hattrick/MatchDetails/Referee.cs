namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    public class Referee
    {
        public uint RefereeCountryId { get; set; }

        public string RefereeCountryName { get; set; } = string.Empty;

        public uint RefereeId { get; set; }

        public string RefereeName { get; set; } = string.Empty;

        public uint RefereeTeamId { get; set; }

        public string RefereeTeamName { get; set; } = string.Empty;
    }
}