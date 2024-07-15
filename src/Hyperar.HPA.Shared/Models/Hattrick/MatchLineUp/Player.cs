namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    public class Player
    {
        public int? Behaviour { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? NickName { get; set; }

        public long PlayerId { get; set; }

        public decimal? RatingStars { get; set; }

        public decimal? RatingStarsEndOfMatch { get; set; }

        public int RoleId { get; set; }
    }
}