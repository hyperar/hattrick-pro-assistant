namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;

    public class Trophy
    {
        public uint TrophyTypeId { get; set; }

        public uint TrophySeason { get; set; }

        public uint LeagueLevel { get; set; }

        public uint LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; } = string.Empty;

        public string GainedDateString { get; set; } = string.Empty;

        public DateTime GainedDate { get; set; }

        public string? ImageUrl { get; set; }

        public uint? CupLeagueLevel { get; set; }

        public uint? CupLevel { get; set; }

        public uint? CupLevelIndex { get; set; }
    }
}
