namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;
    using Domain.Senior;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Models = Shared.Models.Hattrick.ManagerCompendium;

    public class Manager : HattrickEntityBase, IHattrickEntity
    {
        public Manager()
        {
            this.Teams = new HashSet<Team>();
            this.User = new User();

            this.CurrencyName = string.Empty;
            this.UserName = string.Empty;
        }

        public byte[]? AvatarBytes { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string CurrencyName { get; set; }

        public decimal CurrencyRate { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public static Manager Create(Models.Manager xmlManager, byte[]? avatarBytes, Country country, User user)
        {
            return new Manager
            {
                AvatarBytes = avatarBytes,
                Country = country,
                CurrencyName = xmlManager.Currency.CurrencyName,
                CurrencyRate = xmlManager.Currency.CurrencyRate,
                HattrickId = xmlManager.UserId,
                User = user,
                UserName = xmlManager.LoginName,
                SupporterTier = xmlManager.SupporterTier.ToSupporterTier(),
            };
        }

        public bool HasChanged(Models.Manager xmlManager, byte[]? avatarBytes)
        {
            return !Enumerable.SequenceEqual(this.AvatarBytes ?? Array.Empty<byte>(), avatarBytes ?? Array.Empty<byte>())
                || this.CurrencyName != xmlManager.Currency.CurrencyName
                || this.CurrencyRate != xmlManager.Currency.CurrencyRate
                || this.UserName != xmlManager.LoginName
                || this.SupporterTier != xmlManager.SupporterTier.ToSupporterTier();
        }

        public void Update(Models.Manager xmlManager, byte[]? avatarBytes)
        {
            this.AvatarBytes = avatarBytes;
            this.CurrencyName = xmlManager.Currency.CurrencyName;
            this.CurrencyRate = xmlManager.Currency.CurrencyRate;
            this.UserName = xmlManager.LoginName;
            this.SupporterTier = xmlManager.SupporterTier.ToSupporterTier();
        }
    }
}