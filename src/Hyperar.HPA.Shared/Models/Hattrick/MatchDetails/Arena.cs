namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Arena
    {
        public Arena()
        {
            this.ArenaName = string.Empty;
        }

        public long ArenaId { get; set; }

        public string ArenaName { get; set; }

        public int? SoldBasic { get; set; }

        public int? SoldRoof { get; set; }

        public int? SoldTerraces { get; set; }

        public int? SoldTotal { get; set; }

        public int? SoldVip { get; set; }

        public byte? WeatherId { get; set; }
    }
}