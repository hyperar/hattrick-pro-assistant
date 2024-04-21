namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.HallOfFamePlayers;

    public class HallOfFamePlayer : HattrickEntityBase, IHattrickEntity
    {
        public HallOfFamePlayer()
        {
            this.Country = new Country();
            this.Team = new Team();

            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public byte Age { get; set; }

        public virtual Country Country { get; set; }

        public long CountryHattrickId { get; set; }

        public HallOfFameExpertType ExpertType { get; set; }

        public string FirstName { get; set; }

        public DateTime IntroducedOn { get; set; }

        public DateTime JoinedTeamOn { get; set; }

        public string LastName { get; set; }

        public DateTime NextBirthday { get; set; }

        public string? NickName { get; set; }

        public virtual StaffMember? Staff { get; set; }

        public virtual Team Team { get; set; }

        public long TeamHattrickId { get; set; }

        public static HallOfFamePlayer Create(Models.Player xmlPlayer, Country country, Team team)
        {
            return new HallOfFamePlayer
            {
                Age = xmlPlayer.Age,
                Country = country,
                ExpertType = (HallOfFameExpertType)xmlPlayer.ExpertType,
                FirstName = xmlPlayer.FirstName,
                NickName = !string.IsNullOrWhiteSpace(xmlPlayer.NickName) ? xmlPlayer.NickName : null,
                HattrickId = xmlPlayer.PlayerId,
                IntroducedOn = xmlPlayer.HofDate,
                JoinedTeamOn = xmlPlayer.ArrivalDate,
                LastName = xmlPlayer.LastName,
                NextBirthday = xmlPlayer.NextBirthday,
                Team = team
            };
        }

        public bool HasChanged(Models.Player xmlPlayer)
        {
            return this.Age != xmlPlayer.Age
                || this.ExpertType != (HallOfFameExpertType)xmlPlayer.ExpertType
                || this.NextBirthday != xmlPlayer.NextBirthday;
        }

        public void Update(Models.Player xmlPlayer)
        {
            this.Age = xmlPlayer.Age;
            this.ExpertType = (HallOfFameExpertType)xmlPlayer.ExpertType;
            this.NextBirthday = xmlPlayer.NextBirthday;
        }
    }
}