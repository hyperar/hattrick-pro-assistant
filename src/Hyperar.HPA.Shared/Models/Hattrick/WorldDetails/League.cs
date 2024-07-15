namespace Hyperar.HPA.Shared.Models.Hattrick.WorldDetails
{
    using System;
    using System.Collections.Generic;

    public class League
    {
        public int ActiveTeams { get; set; }

        public int ActiveUsers { get; set; }

        public string Continent { get; set; } = string.Empty;

        public Country Country { get; set; } = new Country();

        public DateTime? CupMatchDate { get; set; }

        public List<Cup> Cups { get; set; } = new List<Cup>();

        public DateTime EconomyDate { get; set; }

        public string EnglishName { get; set; } = string.Empty;

        public long LanguageId { get; set; }

        public string LanguageName { get; set; } = string.Empty;

        public long LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public int MatchRound { get; set; }

        public long? NationalTeamId { get; set; }

        public int NumberOfLevels { get; set; }

        public int Season { get; set; }

        public int SeasonOffset { get; set; }

        public DateTime Sequence1 { get; set; }

        public DateTime Sequence2 { get; set; }

        public DateTime Sequence3 { get; set; }

        public DateTime Sequence5 { get; set; }

        public DateTime Sequence7 { get; set; }

        public DateTime SeriesMatchDate { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public DateTime TrainingDate { get; set; }

        public long? U20TeamId { get; set; }

        public int WaitingUsers { get; set; }

        public string ZoneName { get; set; } = string.Empty;
    }
}