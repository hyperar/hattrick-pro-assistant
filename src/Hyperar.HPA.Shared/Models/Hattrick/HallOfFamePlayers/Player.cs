namespace Hyperar.HPA.Shared.Models.Hattrick.HallOfFamePlayers
{
    using System;

    public class Player
    {
        public Player()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public int Age { get; set; }

        public DateTime ArrivalDate { get; set; }

        public long CountryId { get; set; }

        public int ExpertType { get; set; }

        public string FirstName { get; set; }

        public int HofAge { get; set; }

        public DateTime HofDate { get; set; }

        public string LastName { get; set; }

        public DateTime NextBirthday { get; set; }

        public string? NickName { get; set; }

        public long PlayerId { get; set; }
    }
}