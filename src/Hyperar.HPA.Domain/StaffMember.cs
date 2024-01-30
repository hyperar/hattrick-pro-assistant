namespace Hyperar.HPA.Domain
{
    using System;
    using System.Collections.Generic;
    using Common.Enums;
    using Domain.Interfaces;

    public class StaffMember : HattrickEntityBase, IHattrickEntity
    {
        public byte[] Avatar { get; set; } = Array.Empty<byte>();

        public virtual ICollection<StaffMemberAvatarLayer> AvatarLayers { get; set; } = new HashSet<StaffMemberAvatarLayer>();

        public virtual HallOfFamePlayer? HallOfFamePlayer { get; set; }

        public uint? HallOfFamePlayerId { get; set; }

        public DateTime HiredOn { get; set; }

        public uint Level { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint Salary { get; set; }

        public StaffType Type { get; set; }
    }
}