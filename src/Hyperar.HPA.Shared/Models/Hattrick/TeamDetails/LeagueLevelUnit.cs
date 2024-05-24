namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class LeagueLevelUnit
    {
        public LeagueLevelUnit()
        {
            this.LeagueLevelUnitName = string.Empty;
        }

        public int LeagueLevel { get; set; }

        public long LeagueLevelUnitId { get; set; }

        public string LeagueLevelUnitName { get; set; }
    }
}