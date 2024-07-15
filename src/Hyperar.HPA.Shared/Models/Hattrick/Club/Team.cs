namespace Hyperar.HPA.Shared.Models.Hattrick.Club
{
    public class Team
    {
        public Staff Staff { get; set; } = new Staff();

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public YouthSquad YouthSquad { get; set; } = new YouthSquad();
    }
}