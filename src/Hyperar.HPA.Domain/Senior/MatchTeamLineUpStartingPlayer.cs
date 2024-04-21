namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchLineUp;

    public class MatchTeamLineUpStartingPlayer : EntityBase, IEntity
    {
        public MatchTeamLineUpStartingPlayer()
        {
            this.MatchTeamLineUp = new MatchTeamLineUp();

            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public MatchRoleBehavior? Behavior { get; set; }

        public string FirstName { get; set; }

        public long HattrickId { get; set; }

        public string LastName { get; set; }

        public virtual MatchTeamLineUp MatchTeamLineUp { get; set; }

        public int MatchTeamLineUpId { get; set; }

        public string? NickName { get; set; }

        public MatchRole Role { get; set; }

        public static MatchTeamLineUpStartingPlayer Create(Models.StartingPlayer xmlPlayer, MatchTeamLineUp matchTeamLineUp)
        {
            return new MatchTeamLineUpStartingPlayer
            {
                Behavior = xmlPlayer.Behaviour != null ? (MatchRoleBehavior)xmlPlayer.Behaviour : null,
                FirstName = xmlPlayer.FirstName,
                HattrickId = xmlPlayer.PlayerId,
                LastName = xmlPlayer.LastName,
                MatchTeamLineUp = matchTeamLineUp,
                NickName = !string.IsNullOrWhiteSpace(xmlPlayer.NickName) ? xmlPlayer.NickName : null,
                Role = (MatchRole)xmlPlayer.RoleId
            };
        }
    }
}