namespace Hyperar.HPA.Domain.Senior
{
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchTeamBooking : EntityBase, IEntity
    {
        public uint Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public uint Minute { get; set; }

        public uint PlayerHattrickId { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public virtual MatchTeam Team { get; set; } = new MatchTeam();

        public BookingType Type { get; set; }
    }
}