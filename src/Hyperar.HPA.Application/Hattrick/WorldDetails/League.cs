namespace Hyperar.HPA.Application.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;

    public class League
    {
        public uint ActiveTeams { get; set; }

        public uint ActiveUsers { get; set; }

        public string Continent { get; set; } = string.Empty;

        public Country Country { get; set; } = new Country();

        public DateTime CupMatchDate { get; set; }

        public List<Cup> Cups { get; set; } = new List<Cup>();

        public DateTime EconomyDate { get; set; }

        public string EnglishName { get; set; } = string.Empty;

        public uint LanguageId { get; set; }

        public string LanguageName { get; set; } = string.Empty;

        public uint LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public uint MatchRound { get; set; }

        public uint NationalTeamId { get; set; }

        public uint NumberOfLevels { get; set; }

        public uint Season { get; set; }

        public int SeasonOffset { get; set; }

        public DateTime Sequence1 { get; set; }

        public DateTime Sequence2 { get; set; }

        public DateTime Sequence3 { get; set; }

        public DateTime Sequence5 { get; set; }

        public DateTime Sequence7 { get; set; }

        public DateTime SeriesMatchDate { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public DateTime TrainingDate { get; set; }

        public uint U20TeamId { get; set; }

        public uint WaitingUsers { get; set; }

        public string ZoneName { get; set; } = string.Empty;
    }
}