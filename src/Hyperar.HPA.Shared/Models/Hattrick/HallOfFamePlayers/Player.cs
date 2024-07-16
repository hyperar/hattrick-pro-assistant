namespace Hyperar.HPA.Shared.Models.Hattrick.HallOfFamePlayers
{
    using System;

    public class Player
    {
        public int Age { get; set; }

        public DateTime ArrivalDate { get; set; }

        public long CountryId { get; set; }

        public int ExpertType { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public int HofAge { get; set; }

        public DateTime HofDate { get; set; }

        public string LastName { get; set; } = string.Empty;

        public DateTime NextBirthday { get; set; }

        public string? NickName { get; set; }

        public long PlayerId { get; set; }
    }
}