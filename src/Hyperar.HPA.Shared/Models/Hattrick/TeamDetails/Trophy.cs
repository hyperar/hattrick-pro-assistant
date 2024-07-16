namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class Trophy
    {
        public int? CupLeagueLevel { get; set; }

        public int? CupLevel { get; set; }

        public int? CupLevelIndex { get; set; }

        public DateTime GainedDate { get; set; }

        public string? ImageUrl { get; set; }

        public int LeagueLevel { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public int TrophySeason { get; set; }

        public long TrophyTypeId { get; set; }
    }
}