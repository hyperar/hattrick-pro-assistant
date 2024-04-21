namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class SupportedTeam
    {
        public SupportedTeam()
        {
            this.LastMatch = new LastMatch();
            this.NextMatch = new NextMatch();
            this.PressAnnouncement = new PressAnnouncement();

            this.LeagueLevelUnitName = string.Empty;
            this.LeagueName = string.Empty;
            this.LoginName = string.Empty;
            this.TeamName = string.Empty;
        }

        public LastMatch LastMatch { get; set; }

        public long LeagueId { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; }

        public string LeagueName { get; set; }

        public string LoginName { get; set; }

        public NextMatch NextMatch { get; set; }

        public PressAnnouncement PressAnnouncement { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public long UserId { get; set; }
    }
}