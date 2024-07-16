namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Arena
    {
        public long ArenaId { get; set; }

        public string ArenaName { get; set; } = string.Empty;

        public int? SoldBasic { get; set; }

        public int? SoldRoof { get; set; }

        public int? SoldTerraces { get; set; }

        public int? SoldTotal { get; set; }

        public int? SoldVip { get; set; }

        public int? WeatherId { get; set; }
    }
}