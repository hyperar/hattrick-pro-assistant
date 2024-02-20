namespace Hyperar.HPA.Domain
{
    using Common.Enums;
    using Domain.Interfaces;
    using Domain.Senior;

    public class Manager : HattrickEntityBase, IHattrickEntity
    {
        public byte[]? AvatarBytes { get; set; }

        public virtual ICollection<ManagerAvatarLayer> AvatarLayers { get; set; } = new HashSet<ManagerAvatarLayer>();

        public virtual Country Country { get; set; } = new Country();

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();

        public virtual User User { get; set; } = new User();

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}