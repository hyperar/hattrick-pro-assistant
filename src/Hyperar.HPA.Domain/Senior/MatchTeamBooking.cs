namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchTeamBooking : EntityBase, IEntity
    {
        public MatchTeamBooking()
        {
            this.MatchTeam = new MatchTeam();

            this.PlayerName = string.Empty;
        }

        public byte Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeam MatchTeam { get; set; }

        public int MatchTeamId { get; set; }

        public byte Minute { get; set; }

        public long PlayerHattrickId { get; set; }

        public string PlayerName { get; set; }

        public BookingType Type { get; set; }

        public static MatchTeamBooking Create(Models.Booking xmlBooking, MatchTeam matchTeam)
        {
            return new MatchTeamBooking
            {
                Index = xmlBooking.Index,
                MatchPart = (MatchPart)xmlBooking.MatchPart,
                MatchTeam = matchTeam,
                Minute = xmlBooking.BookingMinute,
                PlayerHattrickId = xmlBooking.BookingPlayerId,
                PlayerName = xmlBooking.BookingPlayerName,
                Type = (BookingType)xmlBooking.BookingType
            };
        }
    }
}