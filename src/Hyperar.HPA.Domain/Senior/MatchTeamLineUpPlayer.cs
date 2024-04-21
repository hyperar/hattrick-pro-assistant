namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchLineUp;

    public class MatchTeamLineUpPlayer : EntityBase, IEntity
    {
        public MatchTeamLineUpPlayer()
        {
            this.MatchTeamLineUp = new MatchTeamLineUp();

            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public MatchRoleBehavior? Behavior { get; set; }

        public decimal? EndRating { get; set; }

        public string FirstName { get; set; }

        public long HattrickId { get; set; }

        public string LastName { get; set; }

        public virtual MatchTeamLineUp MatchTeamLineUp { get; set; }

        public int MatchTeamLineUpId { get; set; }

        public string? NickName { get; set; }

        public decimal? Rating { get; set; }

        public MatchRole Role { get; set; }

        public static MatchTeamLineUpPlayer Create(Models.Player xmlPlayer, MatchTeamLineUp matchTeamLineUp)
        {
            return new MatchTeamLineUpPlayer
            {
                Behavior = xmlPlayer.Behaviour != null ? (MatchRoleBehavior)xmlPlayer.Behaviour : null,
                EndRating = xmlPlayer.RatingStarsEndOfMatch,
                FirstName = xmlPlayer.FirstName,
                HattrickId = xmlPlayer.PlayerId,
                LastName = xmlPlayer.LastName,
                MatchTeamLineUp = matchTeamLineUp,
                NickName = !string.IsNullOrWhiteSpace(xmlPlayer.NickName) ? xmlPlayer.NickName : null,
                Rating = xmlPlayer.RatingStars,
                Role = (MatchRole)xmlPlayer.RoleId
            };
        }
    }
}