namespace Hyperar.HPA.Application.Models.Home
{
    using Common.Enums;

    public class Player
    {
        public BookingStatus BookingStatus { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.NickName)
                    ? $"{this.FirstName} {this.LastName}"
                    : $"{this.FirstName} \"{this.NickName}\" {this.LastName}";
            }
        }

        public bool HasMotherClubBonus { get; set; }

        public uint HattrickId { get; set; }

        public int HealthStatus { get; set; }

        public bool IsTransferListed { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string? NickName { get; set; }

        public Specialty Specialty { get; set; }
    }
}