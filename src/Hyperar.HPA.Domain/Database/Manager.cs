namespace Hyperar.HPA.Domain.Database
{
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Interfaces;

    public class Manager : HattrickEntityBase, IEntity, IHattrickEntity
    {
        public SupporterTier SupporterTier { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}