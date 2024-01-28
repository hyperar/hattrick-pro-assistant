namespace Hyperar.HPA.Domain
{
    using System.Collections.Generic;
    using Domain.Interfaces;

    public class League : HattrickEntityBase, IHattrickEntity
    {
        public uint ActiveTeams { get; set; }

        public uint ActiveUsers { get; set; }

        public string Continent { get; set; } = string.Empty;

        public virtual Country? Country { get; set; }

        public virtual ICollection<LeagueCup> Cups { get; set; } = new HashSet<LeagueCup>();

        public string EnglishName { get; set; } = string.Empty;

        public DateTime FifthWeeklyUpdate { get; set; }

        public DateTime FirstWeeklyUpdate { get; set; }

        public byte[] Flag { get; set; } = Array.Empty<byte>();

        public DateTime FourthWeeklyUpdate { get; set; }

        public uint JuniorNationalTeamId { get; set; }

        public uint LanguageId { get; set; }

        public string LanguageName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public DateTime NextCupMatchDate { get; set; }

        public DateTime NextEconomyUpdate { get; set; }

        public DateTime NextSeriesMatchDate { get; set; }

        public DateTime NextTrainingUpdate { get; set; }

        public uint NumberOfLevels { get; set; }

        public uint Season { get; set; }

        public int SeasonOffset { get; set; }

        public DateTime SecondWeeklyUpdate { get; set; }

        public uint NationalTeamId { get; set; }

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();

        public string ShortName { get; set; } = string.Empty;

        public DateTime ThirdWeeklyUpdate { get; set; }

        public uint WaitingUsers { get; set; }

        public uint Week { get; set; }

        public string Zone { get; set; } = string.Empty;
    }
}