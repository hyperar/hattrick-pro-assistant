namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System;

    public class Trophy
    {
        public uint? CupLeagueLevel { get; set; }

        public uint? CupLevel { get; set; }

        public uint? CupLevelIndex { get; set; }

        public DateTime GainedDate { get; set; }

        public string GainedDateString { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public uint LeagueLevel { get; set; }

        public uint LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public uint TrophySeason { get; set; }

        public uint TrophyTypeId { get; set; }
    }
}