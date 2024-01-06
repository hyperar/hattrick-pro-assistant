namespace Hyperar.HPA.Application.Models.HomeView
{
    using Hyperar.HPA.Common.Enums;

    public class SeniorPlayer
    {
        public string FirstName { get; set; } = string.Empty;

        public uint HattrickId { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string? NickName { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public Specialty Specialty { get; set; }

        public bool IsTransferListed { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public int HealthStatus { get; set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(this.NickName)
                ? $"{this.FirstName} {this.LastName}"
                : $"{this.FirstName} \"{this.NickName}\" {this.LastName}";
        }
    }
}