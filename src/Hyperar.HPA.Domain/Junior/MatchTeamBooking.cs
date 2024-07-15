namespace Hyperar.HPA.Domain.Junior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchTeamBooking : AuditableEntityBase, IAuditableEntity
    {
        public int Index { get; set; }

        public long MatchHattrickId { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeam MatchTeam { get; set; } = new MatchTeam();

        public int Minute { get; set; }

        public long PlayerHattrickId { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public long TeamHattrickId { get; set; }

        public BookingType Type { get; set; }
    }
}