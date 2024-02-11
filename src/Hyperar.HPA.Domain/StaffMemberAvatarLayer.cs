namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class StaffMemberAvatarLayer : EntityBase, IEntity
    {
        public string ImageUrl { get; set; } = string.Empty;

        public uint Index { get; set; }

        public virtual StaffMember Staff { get; set; } = new StaffMember();

        public uint XCoordinate { get; set; }

        public uint YCoordinate { get; set; }
    }
}