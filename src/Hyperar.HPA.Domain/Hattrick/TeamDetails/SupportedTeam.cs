namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    public class SupportedTeam
    {
        public uint UserId { get; set; }

        public string LoginName { get; set; } = string.Empty;

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public uint LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public uint LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public LastMatch LastMatch { get; set; } = new LastMatch();

        public NextMatch NextMatch { get; set; } = new NextMatch();

        public PressAnnouncement PressAnnouncement { get; set; } = new PressAnnouncement();
    }
}
