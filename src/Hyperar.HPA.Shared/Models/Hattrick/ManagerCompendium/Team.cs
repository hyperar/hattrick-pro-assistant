namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    public class Team
    {
        public IdName Arena { get; set; } = new IdName();

        public IdName Country { get; set; } = new IdName();

        public League League { get; set; } = new League();

        public IdName LeagueLevelUnit { get; set; } = new IdName();

        public IdName Region { get; set; } = new IdName();

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public YouthTeam? YouthTeam { get; set; }
    }
}