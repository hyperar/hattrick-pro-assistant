namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.StaffList;

    public class StaffMember : HattrickEntityBase, IHattrickEntity
    {
        public StaffMember()
        {
            this.Team = new Team();

            this.AvatarBytes = Array.Empty<byte>();
            this.Name = string.Empty;
        }

        public byte[] AvatarBytes { get; set; }

        public virtual HallOfFamePlayer? HallOfFamePlayer { get; set; }

        public long? HallOfFamePlayerHattrickId { get; set; }

        public DateTime HiredOn { get; set; }

        public byte Level { get; set; }

        public string Name { get; set; }

        public long Salary { get; set; }

        public virtual Team Team { get; set; }

        public long TeamHattrickId { get; set; }

        public StaffType Type { get; set; }

        public static StaffMember Create(Models.Staff xmlStaff, Team team, HallOfFamePlayer? hallOfFamePlayer)
        {
            return new StaffMember
            {
                HallOfFamePlayer = hallOfFamePlayer,
                HattrickId = xmlStaff.StaffId,
                HiredOn = xmlStaff.HiredDate,
                Level = xmlStaff.StaffLevel,
                Name = xmlStaff.Name,
                Salary = xmlStaff.Cost,
                Team = team,
                Type = (StaffType)xmlStaff.StaffType
            };
        }

        public bool HasChanged(Models.Staff xmlStaff)
        {
            return this.HallOfFamePlayerHattrickId != (xmlStaff.HofPlayerId == 0 ? null : xmlStaff.HofPlayerId);
        }

        public void Update(HallOfFamePlayer? hallOfFamePlayer)
        {
            this.HallOfFamePlayer = hallOfFamePlayer;
        }
    }
}