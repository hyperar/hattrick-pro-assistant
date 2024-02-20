namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Common.Enums;
    using Domain.Interfaces;

    public class HallOfFamePlayer : HattrickEntityBase, IHattrickEntity
    {
        public uint Age { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public HallOfFameExpertType ExpertType { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public DateTime IntroducedToHallOfFameOn { get; set; }

        public DateTime JoinedTeamOn { get; set; }

        public string LastName { get; set; } = string.Empty;

        public DateTime NextBirthday { get; set; }

        public string? NickName { get; set; }

        public virtual StaffMember? Staff { get; set; }
    }
}