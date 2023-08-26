namespace Hyperar.HPA.Domain.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;

    public class League
    {
        public uint LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public uint Season { get; set; }

        public int SeasonOffset { get; set; }

        public uint MatchRound { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public string Continent { get; set; } = string.Empty;

        public string ZoneName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

        public uint LanguageId { get; set; }

        public string LanguageName { get; set; } = string.Empty;

        public Country Country { get; set; } = new Country();

        public List<Cup> Cups { get; set; } = new List<Cup>();

        public uint NationalTeamId { get; set; }

        public uint U20TeamId { get; set; }

        public uint ActiveTeams { get; set; }

        public uint ActiveUsers { get; set; }

        public uint WaitingUsers { get; set; }

        public DateTime TrainingDate { get; set; }

        public DateTime EconomyDate { get; set; }

        public DateTime CupMatchDate { get; set; }

        public DateTime SeriesMatchDate { get; set; }

        public DateTime Sequence1 { get; set; }

        public DateTime Sequence2 { get; set; }

        public DateTime Sequence3 { get; set; }

        public DateTime Sequence5 { get; set; }

        public DateTime Sequence7 { get; set; }

        public uint NumberOfLevels { get; set; }
    }
}
