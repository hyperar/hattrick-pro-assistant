namespace Hyperar.HPA.Shared.Models.Hattrick.Club
{
    public class Team
    {
        public Team()
        {
            this.TeamName = string.Empty;

            this.Staff = new Staff();
            this.YouthSquad = new YouthSquad();
        }

        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public Staff Staff { get; set; }

        public YouthSquad YouthSquad { get; set; }
    }
}