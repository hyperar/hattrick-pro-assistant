namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class PlayerAvatarLayer : EntityBase, IEntity
    {
        public string ImageUrl { get; set; } = string.Empty;

        public uint Index { get; set; }

        public virtual Player Player { get; set; } = new Player();

        public uint XCoordinate { get; set; }

        public uint YCoordinate { get; set; }
    }
}