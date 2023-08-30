namespace Hyperar.HPA.Domain.Database
{
    using System.Collections.Generic;
    using Hyperar.HPA.Domain.Interfaces;

    public class League : HattrickEntityBase, IEntity, IHattrickEntity
    {
        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

        public string Continent { get; set; } = string.Empty;

        public string Zone { get; set; } = string.Empty;

        public uint Season { get; set; }

        public int SeasonOffset { get; set; }

        public uint CurrentRound { get; set; }

        public uint LanguageId { get; set; }

        public string LanguageName { get; set; } = string.Empty;

        public uint SeniorNationalTeamId { get; set; }

        public uint JuniorNationalTeamId { get; set; }

        public uint ActiveTeams { get; set; }

        public uint ActiveUsers { get; set; }

        public uint WaitingUsers { get; set; }

        public uint NumberOfLevels { get; set; }

        public virtual Country? Country { get; set; }

        public virtual List<LeagueCup> Cups { get; set; } = new List<LeagueCup>();

        public virtual LeagueCalendar? LeagueCalendar { get; set; }
    }
}
