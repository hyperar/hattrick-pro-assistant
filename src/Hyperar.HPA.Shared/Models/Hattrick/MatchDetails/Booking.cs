namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Booking
    {
        public Booking()
        {
            this.BookingPlayerName = string.Empty;
        }

        public byte BookingMinute { get; set; }

        public long BookingPlayerId { get; set; }

        public string BookingPlayerName { get; set; }

        public long BookingTeamId { get; set; }

        public byte BookingType { get; set; }

        public byte Index { get; set; }

        public byte MatchPart { get; set; }
    }
}