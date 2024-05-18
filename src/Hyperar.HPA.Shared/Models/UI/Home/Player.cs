namespace Hyperar.HPA.Shared.Models.UI.Home
{
    using Shared.Enums;

    public class Player
    {
        public Player()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public long? AskingPrice { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public string FirstName { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public long HattrickId { get; set; }

        public int HealthStatus { get; set; }

        public bool IsTransferListed { get; set; }

        public string LastName { get; set; }

        public string? NickName { get; set; }

        public int? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public long? WinningBid { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {(string.IsNullOrWhiteSpace(this.NickName) ? string.Empty : $"\"{this.NickName}\"")} {this.LastName}";
        }
    }
}