namespace Hyperar.HPA.Domain
{
    using Common.Enums;
    using Domain.Interfaces;

    public class Manager : HattrickEntityBase, IHattrickEntity
    {
        public byte[]? Avatar { get; set; }

        public virtual ICollection<ManagerAvatarLayer> AvatarLayers { get; set; } = new HashSet<ManagerAvatarLayer>();

        public virtual Country Country { get; set; } = new Country();

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public virtual ICollection<SeniorTeam> SeniorTeams { get; set; } = new HashSet<SeniorTeam>();

        public SupporterTier SupporterTier { get; set; }

        public virtual User User { get; set; } = new User();

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}