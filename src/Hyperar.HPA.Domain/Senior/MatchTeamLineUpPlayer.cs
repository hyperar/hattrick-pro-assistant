namespace Hyperar.HPA.Domain.Senior
{
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchTeamLineUpPlayer : EntityBase, IEntity
    {
        public MatchRoleBehavior? Behavior { get; set; }

        public decimal? EndRating { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public uint HattrickId { get; set; }

        public string LastName { get; set; } = string.Empty;

        public virtual MatchTeamLineUp LineUp { get; set; } = new MatchTeamLineUp();

        public string? NickName { get; set; }

        public decimal? Rating { get; set; }

        public ushort Role { get; set; }
    }
}