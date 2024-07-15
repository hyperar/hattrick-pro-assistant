namespace Hyperar.HPA.Domain.Junior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchTeamLineUpPlayer : AuditableEntityBase, IAuditableEntity
    {
        public decimal? AverageRating { get; set; }

        public MatchRoleBehavior? Behavior { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public long MatchHattrickId { get; set; }

        public virtual MatchTeamLineUp MatchTeamLineUp { get; set; } = new MatchTeamLineUp();

        public string? NickName { get; set; }

        public long PlayerHattrickId { get; set; }

        public MatchRole Role { get; set; }

        public long TeamHattrickId { get; set; }
    }
}