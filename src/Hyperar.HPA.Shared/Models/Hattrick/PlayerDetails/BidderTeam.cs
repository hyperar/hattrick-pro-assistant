namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    public class BidderTeam
    {
        public BidderTeam()
        {
            this.TeamName = string.Empty;
        }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}