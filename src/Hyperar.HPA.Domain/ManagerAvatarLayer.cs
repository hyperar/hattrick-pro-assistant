namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public class ManagerAvatarLayer : EntityBase, IEntity
    {
        public string ImageUrl { get; set; } = string.Empty;

        public uint Index { get; set; }

        public virtual Manager Manager { get; set; } = new Manager();

        public uint XCoordinate { get; set; }

        public uint YCoordinate { get; set; }
    }
}