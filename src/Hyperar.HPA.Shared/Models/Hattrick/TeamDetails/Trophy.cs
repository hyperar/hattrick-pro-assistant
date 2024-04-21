namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class Trophy
    {
        public Trophy()
        {
            this.LeagueLevelUnitName = string.Empty;
        }

        public byte? CupLeagueLevel { get; set; }

        public byte? CupLevel { get; set; }

        public byte? CupLevelIndex { get; set; }

        public DateTime GainedDate { get; set; }

        public string? ImageUrl { get; set; }

        public byte LeagueLevel { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; }

        public byte TrophySeason { get; set; }

        public long TrophyTypeId { get; set; }
    }
}