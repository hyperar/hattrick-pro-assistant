namespace Hyperar.HPA.Shared.Models.Hattrick.YouthTeamDetails
{
    using System;

    public class Scout
    {
        public int Age { get; set; }

        public IdName Country { get; set; } = new IdName();

        public DateTime HiredDate { get; set; }

        public long HofPlayerId { get; set; }

        public IdName InCountry { get; set; } = new IdName();

        public IdName InRegion { get; set; } = new IdName();

        public DateTime LastCalled { get; set; }

        public int PlayerTypeSearch { get; set; }

        public IdName Region { get; set; } = new IdName();

        public string ScoutName { get; set; } = string.Empty;

        public Travel? Travel { get; set; }

        public long YouthScoutId { get; set; }
    }
}