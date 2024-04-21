namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    public class Player
    {
        public Player()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.NickName = string.Empty;
        }

        public short? Behaviour { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public long PlayerId { get; set; }

        public decimal? RatingStars { get; set; }

        public decimal? RatingStarsEndOfMatch { get; set; }

        public short RoleId { get; set; }
    }
}