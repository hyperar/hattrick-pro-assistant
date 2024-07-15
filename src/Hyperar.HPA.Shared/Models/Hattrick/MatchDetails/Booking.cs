namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Booking
    {
        public int BookingMinute { get; set; }

        public long BookingPlayerId { get; set; }

        public string BookingPlayerName { get; set; } = string.Empty;

        public long BookingTeamId { get; set; }

        public int BookingType { get; set; }

        public int Index { get; set; }

        public int MatchPart { get; set; }
    }
}