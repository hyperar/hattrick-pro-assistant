namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class Manager : HattrickEntityBase, IHattrickEntity
    {
        public byte[] AvatarBytes { get; set; } = Array.Empty<byte>();

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public virtual ICollection<Senior.Team> SeniorTeams { get; set; } = new HashSet<Senior.Team>();

        public SupporterTier SupporterTier { get; set; }

        public virtual User User { get; set; } = new User();

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}