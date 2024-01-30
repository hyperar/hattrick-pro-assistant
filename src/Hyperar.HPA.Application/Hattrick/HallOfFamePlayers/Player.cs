namespace Hyperar.HPA.Application.Hattrick.HallOfFamePlayers
{
    using System;
    using Common.Enums;

    public class Player
    {
        public uint Age { get; set; }

        public DateTime ArrivalDate { get; set; }

        public uint CountryId { get; set; }

        public HallOfFameExpertType ExpertType { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public uint HofAge { get; set; }

        public DateTime HofDate { get; set; }

        public string LastName { get; set; } = string.Empty;

        public DateTime NextBirthday { get; set; }

        public string? NickName { get; set; } = string.Empty;

        public uint PlayerId { get; set; }
    }
}