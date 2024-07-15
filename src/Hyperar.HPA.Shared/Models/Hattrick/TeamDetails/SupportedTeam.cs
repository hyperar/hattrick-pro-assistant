namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class SupportedTeam
    {
        public LastMatch LastMatch { get; set; } = new LastMatch();

        public long LeagueId { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public string LeagueName { get; set; } = string.Empty;

        public string LoginName { get; set; } = string.Empty;

        public NextMatch NextMatch { get; set; } = new NextMatch();

        public PressAnnouncement PressAnnouncement { get; set; } = new PressAnnouncement();

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public long UserId { get; set; }
    }
}