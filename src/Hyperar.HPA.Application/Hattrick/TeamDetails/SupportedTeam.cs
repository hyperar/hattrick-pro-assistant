namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    public class SupportedTeam
    {
        public LastMatch LastMatch { get; set; } = new LastMatch();

        public uint LeagueId { get; set; }

        public uint LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public string LeagueName { get; set; } = string.Empty;

        public string LoginName { get; set; } = string.Empty;

        public NextMatch NextMatch { get; set; } = new NextMatch();

        public PressAnnouncement PressAnnouncement { get; set; } = new PressAnnouncement();

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public uint UserId { get; set; }
    }
}