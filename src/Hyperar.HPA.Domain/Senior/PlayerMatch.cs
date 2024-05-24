namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick;

    public class PlayerMatch : EntityBase, IEntity
    {
        public PlayerMatch()
        {
            this.Player = new Player();

            this.CalculatedRating = string.Empty;
        }

        public decimal AverageRating { get; set; }

        public string CalculatedRating { get; set; }

        public DateTime Date { get; set; }

        public decimal EndOfMatchRating { get; set; }

        public long MatchHattrickId { get; set; }

        public virtual Player Player { get; set; }

        public long PlayerHattrickId { get; set; }

        public MatchRole Role { get; set; }

        public static PlayerMatch Create(
            Models.PlayerDetails.LastMatch lastMatch,
            Player player)
        {
            return new PlayerMatch
            {
                Date = lastMatch.Date,
                MatchHattrickId = lastMatch.MatchId,
                Role = (MatchRole)lastMatch.PositionCode,
                AverageRating = lastMatch.Rating,
                EndOfMatchRating = lastMatch.RatingEndOfMatch,
                Player = player
            };
        }

        public static PlayerMatch Create(
            Models.MatchLineUp.Player lineUpPlayer,
            Player player,
            long matchId,
            DateTime date,
            string calculatedRating)
        {
            ArgumentNullException.ThrowIfNull(lineUpPlayer.RatingStars, nameof(lineUpPlayer.RatingStars));
            ArgumentNullException.ThrowIfNull(lineUpPlayer.RatingStarsEndOfMatch, nameof(lineUpPlayer.RatingStarsEndOfMatch));

            return new PlayerMatch
            {
                CalculatedRating = calculatedRating,
                Date = date,
                MatchHattrickId = matchId,
                Role = (MatchRole)lineUpPlayer.RoleId,
                AverageRating = lineUpPlayer.RatingStars.Value,
                EndOfMatchRating = lineUpPlayer.RatingStarsEndOfMatch.Value,
                Player = player
            };
        }
    }
}