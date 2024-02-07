namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Hyperar.HPA.Common.Enums;

    public class Booking
    {
        public uint BookingMinute { get; set; }

        public uint BookingPlayerId { get; set; }

        public string BookingPlayerName { get; set; } = string.Empty;

        public uint BookingTeamId { get; set; }

        public BookingType BookingType { get; set; }

        public uint Index { get; set; }

        public MatchPart MatchPart { get; set; }
    }
}