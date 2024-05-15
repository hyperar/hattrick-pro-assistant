namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    public class MotherClub
    {
        public MotherClub()
        {
            this.TeamName = string.Empty;
        }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}