namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    public class Team
    {
        public Team()
        {
            this.Arena = new IdName();
            this.Country = new IdName();
            this.League = new League();
            this.LeagueLevelUnit = new IdName();
            this.Region = new IdName();

            this.TeamName = string.Empty;
        }

        public IdName Arena { get; set; }

        public IdName Country { get; set; }

        public League League { get; set; }

        public IdName LeagueLevelUnit { get; set; }

        public IdName Region { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public YouthTeam? YouthTeam { get; set; }
    }
}