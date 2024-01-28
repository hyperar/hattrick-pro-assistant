namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class SeniorPlayerAvatarLayer : EntityBase, IEntity
    {
        public string ImageUrl { get; set; } = string.Empty;

        public uint Index { get; set; }

        public virtual SeniorPlayer SeniorPlayer { get; set; } = new SeniorPlayer();

        public uint XCoordinate { get; set; }

        public uint YCoordinate { get; set; }
    }
}