namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    public class LeagueLevelUnit
    {
        public LeagueLevelUnit()
        {
            this.LeagueLevelUnitName = string.Empty;
        }

        public long LeagueLevel { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; }
    }
}